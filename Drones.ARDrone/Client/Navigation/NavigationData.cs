using Drones.ARDrone.Data.Navdata;
using Drones.ARDrone.Extensions;
using Drones.Client.Navigation;
using System;

namespace Drones.ARDrone.Client.Navigation
{
    public class NavigationData : NavigationDataBase
    {
        NavigationState _state = NavigationState.Unknown;
        public NavigationState State
        {
            get
            {
                return _state;
            }
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    RaisePropertyChanged();
                }
            }
        }


        // @Public
        public static NavigationData FromNavdataPacket(NavdataPacket packet)
        {
            var navigationData = new NavigationData();
            navigationData.UpdateState(packet.Header.DroneState);

            // Demo
            if (packet.Demo != null)
            {
                navigationData.UpdateState(packet.Demo.State);

                navigationData.Pitch = _degreeToRadian * (packet.Demo.Theta / 1000.0f);
                navigationData.Roll = _degreeToRadian * (packet.Demo.Phi / 1000.0f);
                navigationData.Yaw = _degreeToRadian * (packet.Demo.Psi / 1000.0f);
                navigationData.Altitude = new Distance(packet.Demo.Altitude / 100.0f);

                navigationData.Speed = new Speed()
                {
                    X = packet.Demo.VX / 1000.0f,
                    Y = packet.Demo.VY / 1000.0f,
                    Z = packet.Demo.VZ / 1000.0f,
                    MeasurementUnit = SpeedMeasurementUnit.MetersPerSecond
                };

                navigationData.Battery.IsLow = packet.Header.DroneState.HasFlag(DroneStateMask.VBatLow);
                navigationData.Battery.Percentage = packet.Demo.VBatFlyingPercentage;
            }

            // Raw mesures.
            if (packet.RawMesures != null)
            {
                navigationData.Battery.Voltage = packet.RawMesures.VBatRaw / 1000.0f;
            }

            // Magneto.
            if (packet.Magneto != null)
            {
                navigationData.Magneto.Rectified.X = packet.Magneto.MagnetoRectified.X;
                navigationData.Magneto.Rectified.Y = packet.Magneto.MagnetoRectified.Y;
                navigationData.Magneto.Rectified.Z = packet.Magneto.MagnetoRectified.Z;

                navigationData.Magneto.Offset.X = packet.Magneto.MagnetoOffset.X;
                navigationData.Magneto.Offset.Y = packet.Magneto.MagnetoOffset.Y;
                navigationData.Magneto.Offset.Z = packet.Magneto.MagnetoOffset.Z;
            }

            // Video.
            if (packet.VideoStream != null)
            {
                navigationData.Video.FrameNumber = packet.VideoStream.FrameNumber;
                navigationData.Video.BitRate = packet.VideoStream.OutBitrate;
            }

            // Wifi
            if (packet.Wifi != null)
            {
                navigationData.Communication.Type = CommunicationType.Wifi;
                navigationData.Communication.LinkQuality = 1.0f - packet.Wifi.LinkQuality.ToFloat();
            }

            return navigationData;
        }


        // @Private
        const float _degreeToRadian = (float)(Math.PI / 180.0f);

        void UpdateState(DroneStateMask droneState)
        {
            if (droneState.HasFlag(DroneStateMask.NavdataBootstrap))
            {
                State |= NavigationState.Bootstrap;
            }

            if (droneState.HasFlag(DroneStateMask.FlyMask))
            {
                State |= NavigationState.Flying;
            }
            else
            {
                State |= NavigationState.Landed;
            }

            if (droneState.HasFlag(DroneStateMask.WindMask))
            {
                State |= NavigationState.Wind;
            }

            if (droneState.HasFlag(DroneStateMask.EmergencyMask))
            {
                State |= NavigationState.Emergency;
            }

            if (droneState.HasFlag(DroneStateMask.CommandMask))
            {
                State |= NavigationState.Command;
            }

            if (droneState.HasFlag(DroneStateMask.ControlMask))
            {
                State |= NavigationState.Control;
            }

            if (droneState.HasFlag(DroneStateMask.ComWatchdogMask))
            {
                State |= NavigationState.Watchdog;
            }
        }

        void UpdateState(ControlState controlState)
        {
            switch (controlState)
            {
                case ControlState.TransTakeOff:
                    State |= NavigationState.Takeoff;
                    break;
                case ControlState.TransLanding:
                    State |= NavigationState.Landing;
                    break;
                case ControlState.Hovering:
                    State |= NavigationState.Hovering;
                    break;
            }
        }
    }
}
