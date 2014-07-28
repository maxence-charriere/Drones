using Drones.ARDrone.Client.ATCommands;
using Drones.ARDrone.Client.Navigation;
using Drones.ARDrone.Data.Navdata;
using Drones.ARDrone.Extensions;
using Drones.Client;
using Drones.Infrastructure;
using System;
using System.Collections.Generic;
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

            // ACK CONTROL MODE
            
            // CONFIG
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
            throw new NotImplementedException();
        }

        public void Land()
        {
            throw new NotImplementedException();
        }

        public void Emergency()
        {
            throw new NotImplementedException();
        }

        public void EmergencyRecover()
        {
            throw new NotImplementedException();
        }

        public void Move(float roll, float pitch, float gaz, float yaw)
        {
            throw new NotImplementedException();
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
                // IMPLEMENT SETTING INIT.
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
                // IMPLEMENT INPUT MECHANISM.
            }

            // State transitions.
            switch (requestedState)
            {
                case RequestedState.None:
                    return;
                case RequestedState.Land:
                    if (navigationState.HasFlag(NavigationState.Flying)
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
                        ATCommandSender.Send(RefCommand.Emergency);
                    }
                    if (navigationState.HasFlag(NavigationState.Takeoff) == false
                        && navigationState.HasFlag(NavigationState.Emergency) == false)
                    {
                        ATCommandSender.Send(RefCommand.Land);
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
                    ATCommandSender.Send(RefCommand.Emergency);
                    RequestedState = RequestedState.None;
                    break;
                default:
                    break;
            }
        }
    }
}
