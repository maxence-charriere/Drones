using Drones.ARDrone.Client.ATCommands;
using Drones.ARDrone.Client.Navigation;
using Drones.ARDrone.Client.Video;
using Drones.ARDrone.Data.Configuration;
using Drones.ARDrone.Data.Navdata;
using Drones.ARDrone.Data.Video;
using Drones.ARDrone.Extensions;
using Drones.Client;
using Drones.Client.Configuration;
using Drones.Client.Navigation;
using Drones.Client.Video;
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
        public event Action<INavigationData> NavigationDataAcquired;
        public event Action<VideoFrame> VideoFrameDecoded;


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
                return NavdataReceiver.IsAcquiring;
            }
        }

        INavigationData _currentNavigationData;
        public INavigationData CurrentNavigationData
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

        Settings _settings;
        public Settings Settings
        {
            get
            {
                lock (_settingsSync)
                {
                    return _settings;
                }
            }
            set
            {
                lock (_settingsSync)
                {
                    _settings = value;
                    _droneConfiguration.Update(_settings);
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
        public readonly NavdataReceiver NavdataReceiver;
        public readonly ATCommandSender ATCommandSender;
        public readonly VideoReceiver VideoReceiver;
        public readonly VideoDecoderWorker VideoDecoderWorker;

        public ARDroneClient(string hostname = "192.168.1.1")
        {
            Hostname = hostname;
            NavdataReceiver = new NavdataReceiver(Hostname);
            NavdataReceiver.NavdataAcquisitionStarted += OnNavdataAcquisitionStarted;
            NavdataReceiver.NavdataAcquisitionStoped += OnNavdataAcquisitionStopped;
            NavdataReceiver.NavdataPacketAcquired += OnNavdataPacketAcquired;

            ATCommandSender = new ATCommandSender(Hostname);
            CurrentNavigationData = new NavigationData();

            VideoReceiver = new VideoReceiver(Hostname);
            VideoReceiver.VideoPacketAcquired += OnVideoPacketAcquired;

            VideoDecoderWorker = new VideoDecoderWorker(VideoPixelFormat.BGR24);
            VideoDecoderWorker.FrameDecoded += RaiseVideoFrameDecoded;
        }

        public async Task<bool> ConnectAsync()
        {
            Debug.WriteLine(string.Format("Connect to drone on {0}...", Hostname));
            if (IsAlive)
            {
                Debug.WriteLine("Connected.");
                return true;
            }

            // Launching workers.
            Start();

            Stopwatch swConnect = Stopwatch.StartNew();
            while (swConnect.ElapsedMilliseconds < _connectTimeout)
            {
                if (IsConnected)
                {
                    await InitMulticonfigurationAsync();
                    //_droneConfiguration.Video.Bitrate = 30;
                    _droneConfiguration.General.NavdataOptions = NavdataOptionFlag.Demo
                        | NavdataOptionFlag.RawMeasures
                        | NavdataOptionFlag.Magneto
                        | NavdataOptionFlag.VideoStream
                        | NavdataOptionFlag.WiFi
                        | NavdataOptionFlag.Pwm
                        | NavdataOptionFlag.Wind;
                    Debug.WriteLine("Connected.");
                    return true;
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            Stop();
            return false;
        }

        public void Disconnect()
        {
            Debug.WriteLine("Disconnect...");
            Stop();
            Debug.WriteLine("Disconnected.");
        }

        public void TakeOff()
        {
            Debug.WriteLine("Take off.");
            RequestedState = RequestedState.Fly;
        }

        public void Land()
        {
            Debug.WriteLine("Land.");
            RequestedState = RequestedState.Land;
        }

        public void Emergency()
        {
            Debug.WriteLine("Emergency.");
            RequestedState = RequestedState.Emergency;
        }

        public void EmergencyRecover()
        {
            Debug.WriteLine("Recover emergency.");
            RequestedState = RequestedState.ResetEmergency;
        }

        public void CalibrateMagneto()
        {
            Debug.WriteLine("Calibrate magnetometer.");
            ATCommandSender.Send(CalibCommand.Magnetometer);
        }

        public void SwitchVideoChannel()
        {
            Debug.WriteLine("Switch video channel.");
            _droneConfiguration.Video.Channel = VideoChannelType.Next;
        }

        public void Move(float roll, float pitch, float gaz, float yaw)
        {
            Debug.WriteLine(string.Format("Move (p: {0}, roll: {1}, gaz: {2}, yaw: {3}).)", pitch, roll, gaz, yaw));
            ATCommandSender.Send(new PCmdCommand(FlightMode.Progressive, roll, pitch, gaz, yaw));
        }

        public async Task FlipAsync(FlipType type)
        {
            switch (type)
            {
                case FlipType.Front:
                    _droneConfiguration.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.FlipAhead);
                    break;
                case FlipType.Back:
                    _droneConfiguration.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.FlipBehind);
                    break;
                case FlipType.Left:
                    _droneConfiguration.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.FlipLeft);
                    break;
                case FlipType.Right:
                    _droneConfiguration.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.FlipRight);
                    break;
                default:
                    return;
            }
            await Task.Delay(_droneConfiguration.Control.FlightAnimation.Duration);
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                // Starts or restarts Navdata receiver.
                if (NavdataReceiver.IsAlive == false)
                {
                    NavdataReceiver.Start();
                }
                else
                {
                    /*// Restarts Command sender.
                    if (ATCommandSender.IsAlive == false)
                    {
                        ATCommandSender.Start();
                    }

                    // Restarts Video receiver.
                    if (VideoReceiver.IsAlive == false)
                    {
                        //VideoReceiver.Start();
                    }*/
                }

                // Process commands.
                if (ATCommandSender.IsAlive && CurrentNavigationData != null)
                {
                    ProcessCommands(RequestedState, ((NavigationData)CurrentNavigationData).State);
                }

                
                Thread.Sleep(25);
            }

            NavdataReceiver.Stop();
            ATCommandSender.Stop();
            VideoReceiver.Stop();
            VideoDecoderWorker.Stop();
        }

        protected override void DisposeOverride()
        {
            base.DisposeOverride();

            NavdataReceiver.Dispose();
            ATCommandSender.Dispose();
            VideoReceiver.Dispose();
            VideoDecoderWorker.Dispose();
        }


        // @Private
        const int _connectTimeout = 5000;
        const int _ackControlAndWaitForConfirmationTimeout = 1000;
        readonly object _navigationDataSync = new object();
        readonly object _stateRequestSync = new object();
        readonly object _settingsSync = new object();
        Config _droneConfiguration = new Config();
        double _videoPacketInterval = 0;
        double _lastLinkQuality = 0;
        Stopwatch _swVideoPacketInterval;
        Stopwatch _swWifi;

        void OnNavdataAcquisitionStarted()
        {
            // Starting workers.
            if (ATCommandSender.IsAlive == false)
            {
                ATCommandSender.Start();
            }
            if (VideoReceiver.IsAlive == false)
            {
                VideoReceiver.Start();
                _swWifi = Stopwatch.StartNew();
                _swVideoPacketInterval = Stopwatch.StartNew();
            }
        }

        void OnNavdataAcquisitionStopped()
        {
            // Stopping workers.
            ATCommandSender.Stop();
            VideoReceiver.Stop();
        }

        void OnNavdataPacketAcquired(NavdataPacket packet)
        {
            if (packet.IsValid)
            {
                var navigationData = NavigationData.FromNavdataPacket(packet);
                CurrentNavigationData = navigationData;
                navigationData.Communication.LinkQuality = _lastLinkQuality;
                if (_swWifi.ElapsedMilliseconds > 1000)
                {
                    _lastLinkQuality = 100 - _videoPacketInterval;
                    _swWifi.Restart();
                }
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

        void OnVideoPacketAcquired(VideoPacket packet)
        {
            if (VideoDecoderWorker.IsAlive == false)
            {
                VideoDecoderWorker.Start();
            }

            VideoDecoderWorker.EnqueueVideoPacket(packet);
        }

        void RaiseVideoFrameDecoded(VideoFrame videoFrame)
        {
            _videoPacketInterval = Math.Round((_swVideoPacketInterval.ElapsedMilliseconds + _videoPacketInterval) / 2);
            //Debug.WriteLine("Video Packet interval = " + _videoPacketInterval);
            _swVideoPacketInterval.Restart();

            if (VideoFrameDecoded != null)
            {
                VideoFrameDecoded(videoFrame);
            }
        }

        async Task InitMulticonfigurationAsync()
        {
            Debug.WriteLine("Initializing multiconfiguration...");
            await AckControlAndWaitForConfirmationAsync();
            _droneConfiguration.Custom.SessionId = Config.NewId();
            ATCommandSender.Send(_droneConfiguration);

            await AckControlAndWaitForConfirmationAsync();
            _droneConfiguration.Custom.ProfileId = Config.NewId();
            ATCommandSender.Send(_droneConfiguration);

            await AckControlAndWaitForConfirmationAsync();
            _droneConfiguration.Custom.ApplicationId = Config.NewId();
            ATCommandSender.Send(_droneConfiguration);

            await AckControlAndWaitForConfirmationAsync();
            Debug.WriteLine("Multiconfiguration initalized.");
        }

        async Task<bool> AckControlAndWaitForConfirmationAsync()
        {
            Stopwatch swTimeout = Stopwatch.StartNew();

            var state = NavigationState.Unknown;
            Action<INavigationData> onNavigationData = nd => state = ((NavigationData)nd).State;
            NavigationDataAcquired += onNavigationData;
            try
            {
                bool ackControlSent = false;
                while (swTimeout.ElapsedMilliseconds < _ackControlAndWaitForConfirmationTimeout)
                {
                    if (state.HasFlag(NavigationState.Command))
                    {
                        ATCommandSender.Send(ControlCommand.AckControlMode);
                        ackControlSent = true;
                    }

                    if (ackControlSent && state.HasFlag(NavigationState.Command) == false)
                    {
                        return true;
                    }
                    await Task.Delay(5);
                }
                return false;
            }
            finally
            {
                NavigationDataAcquired -= onNavigationData;
                Debug.WriteLine(string.Format("AckCommand done in {0} ms.", swTimeout.ElapsedMilliseconds));
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

            // Watchdog.
            if (navigationState.HasFlag(NavigationState.Watchdog))
            {
                ATCommandSender.Send(ComWdgCommand.Default);
            }

            // Config.
            if (_droneConfiguration.Changes.IsEmpty == false)
            {
                ATCommandSender.Send(_droneConfiguration);
            }

            // Input.
            if (requestedState == RequestedState.None)
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
