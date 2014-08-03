using Drones.ARDrone.Client.ATCommands;
using Drones.ARDrone.Client.Navigation;
using Drones.ARDrone.Data.Configuration;
using Drones.ARDrone.Data.Navdata;
using Drones.ARDrone.Extensions;
using Drones.Client;
using Drones.Infrastructure;
using Drones.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client
{
    public class ARDroneClient : WorkerBase, IDroneClient
    {
        // @Events
        public event Action<NavigationData> NavigationDataAcquired;


        // @Properties
        public bool IsActive
        {
            get
            {
                return IsAlive;
            }
        }

        public bool IsConnected
        {
            get
            {
                return NavdataAcquisition.IsAcquiring;
            }
        }

        NavigationData _currentNavigationData;
        public NavigationData CurrentNavigationData
        {
            get
            {
                lock (_navigationDataSync)
                {
                    return _currentNavigationData;
                }
            }
            internal set
            {
                lock (_navigationDataSync)
                {
                    _currentNavigationData = value;
                }
            }
        }

        RequestedState _requestedState = RequestedState.None;
        public RequestedState RequestedState
        {
            get
            {
                lock (_stateRequestSync)
                {
                    return _requestedState;
                }
            }
            set
            {
                lock (_stateRequestSync)
                {
                    _requestedState = value;
                }
            }
        }

        XBox360Input _xBox360Input = new XBox360Input();
        public XBox360Input XBox360Input
        {
            get
            {
                return _xBox360Input;
            }
            set
            {
                _xBox360Input = value;
            }
        }


        // @Public
        public readonly string Hostname;
        public readonly NavdataAcquisition NavdataAcquisition;
        public readonly ATCommandSender ATCommandSender;

        public ARDroneClient(string hostname = "192.168.1.1")
        {
            Hostname = hostname;
            NavdataAcquisition = new NavdataAcquisition(Hostname);
            NavdataAcquisition.NavdataAcquisitionStarted += OnNavdataAcquisitionStarted;
            NavdataAcquisition.NavdataAcquisitionStoped += OnNavdataAcquisitionStopped;
            NavdataAcquisition.NavdataPacketAcquired += OnNavdataPacketAcquired;

            ATCommandSender = new ATCommandSender(Hostname);
            CurrentNavigationData = new NavigationData();
        }

        public async Task<bool> ConnectAsync()
        {
            // Launching workers.
            Start();

            Stopwatch swConnect = Stopwatch.StartNew();
            while (swConnect.ElapsedMilliseconds < _connectTimeout)
            {
                if (IsConnected)
                {
                    return true;
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            return false;
        }

        public void Disconnect()
        {
            Stop();
            NavdataAcquisition.Stop();
            ATCommandSender.Stop();
        }

        public void TakeOff()
        {
            RequestedState = RequestedState.Fly;
        }

        public void Land()
        {
            RequestedState = RequestedState.Land;
        }

        public void Emergency()
        {
            RequestedState = RequestedState.Emergency;
        }

        public void EmergencyRecover()
        {
            RequestedState = RequestedState.ResetEmergency;
        }

        public void Move(float roll, float pitch, float gaz, float yaw)
        {
            ATCommandSender.Send(new PCmdCommand(FlightMode.Progressive, roll, pitch, gaz, yaw));
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!NavdataAcquisition.IsAlive)
                {
                    NavdataAcquisition.Start();
                }

                if (ATCommandSender.IsAlive && CurrentNavigationData != null)
                {
                    ProcessCommands(RequestedState, CurrentNavigationData.State);
                }
                Thread.Sleep(25);
            }

            // Stop Navdata acquisition.
            if (NavdataAcquisition.IsAlive)
            {
                NavdataAcquisition.Stop();
            }

            // Stop sending AT commands.
            if (ATCommandSender.IsAlive)
            {
                ATCommandSender.Stop();
            }

        }

        protected override void DisposeOverride()
        {
            base.DisposeOverride();

            NavdataAcquisition.Dispose();
            ATCommandSender.Dispose();
        }


        // @Private
        readonly object _navigationDataSync = new object();
        readonly object _stateRequestSync = new object();
        Configuration _droneConfiguration = new Configuration();
        const int _connectTimeout = 5000;

        void OnNavdataAcquisitionStarted()
        {
            // Starting workers.
            if (!ATCommandSender.IsAlive)
            {
                ATCommandSender.Start();
            }
        }

        void OnNavdataAcquisitionStopped()
        {
            // Stopping workers.
            ATCommandSender.Stop();
        }

        void OnNavdataPacketAcquired(NavdataPacket packet)
        {
            if (packet.IsValid)
            {
                var navigationData = NavigationData.FromNavdataPacket(packet);
                CurrentNavigationData = navigationData;
                RaiseNavigationDataAcquired(navigationData);
            }
        }

        void RaiseNavigationDataAcquired(NavigationData navigationData)
        {
            if (NavigationDataAcquired != null)
            {
                NavigationDataAcquired(navigationData);
            }
        }

        void ProcessCommands(RequestedState requestedState, NavigationState navigationState)
        {
            // Bootstrap.
            if (navigationState.HasFlag(NavigationState.Bootstrap))
            {
                ATCommandSender.CommandQueue.Flush();
                _droneConfiguration.General.NavdataDemo = true;
                ATCommandSender.Send(_droneConfiguration);
            }

            // Command.
            if (navigationState.HasFlag(NavigationState.Command))
            {
                ATCommandSender.Send(ControlCommand.AckControlMode);
            }

            // Watchdog.
            if (navigationState.HasFlag(NavigationState.Watchdog))
            {
                ATCommandSender.Send(ComWdgCommand.Default);
            }

            // Input.
            if (requestedState == RequestedState.None && navigationState.HasFlag(NavigationState.Flying))
            {
                bool isHovering = true;
                if (XBox360Input.IsConnected)
                {
                    XBox360Input.Update();
                    if (XBox360Input.IsMotionless == false)
                    {
                        isHovering = false;
                        ATCommandSender.Send(new PCmdCommand(FlightMode.Progressive,
                            XBox360Input.Roll,
                            XBox360Input.Pitch,
                            XBox360Input.Gaz,
                            XBox360Input.Yaw));
                    }
                }

                if (isHovering)
                {
                    ATCommandSender.Send(new PCmdCommand(FlightMode.Hover, 0, 0, 0, 0));
                }
            }

            // State transitions.
            switch (requestedState)
            {
                case RequestedState.None:
                    return;
                case RequestedState.Land:
                    if ((navigationState.HasFlag(NavigationState.Flying) || navigationState.HasFlag(NavigationState.Takeoff)) 
                        && navigationState.HasFlag(NavigationState.Landing) == false)
                    {
                        ATCommandSender.Send(RefCommand.Land);
                    }
                    else
                    {
                        RequestedState = RequestedState.None;
                    }
                    break;
                case RequestedState.Fly:
                    if (navigationState.HasFlag(NavigationState.Emergency))
                    {
                        ATCommandSender.Send(FTrimCommand.Default);
                        ATCommandSender.Send(RefCommand.Emergency);
                    }
                    if (navigationState.HasFlag(NavigationState.Landed)
                        && navigationState.HasFlag(NavigationState.Takeoff) == false
                        && navigationState.HasFlag(NavigationState.Emergency) == false)
                    {
                        ATCommandSender.Send(RefCommand.TakeOff);
                    }
                    else
                    {
                        RequestedState = RequestedState.None;
                    }
                    break;
                case RequestedState.Emergency:
                    if (navigationState.HasFlag(NavigationState.Flying))
                    {
                        ATCommandSender.Send(RefCommand.Emergency);
                    }
                    else
                    {
                        RequestedState = RequestedState.None;
                    }
                    break;
                case RequestedState.ResetEmergency:
                    ATCommandSender.Send(FTrimCommand.Default);
                    ATCommandSender.Send(RefCommand.Emergency);
                    RequestedState = RequestedState.None;
                    break;
                default:
                    break;
            }
        }
    }
}
