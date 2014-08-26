using Drones.ARDrone.Data.Navdata;
using Drones.ARDrone.Extensions;
using Drones.Client.Navigation;
using System;
using System.Diagnostics;

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

                navigationData.Pitch = packet.Demo.Theta / 1000;
                navigationData.Roll = packet.Demo.Phi / 1000;
                navigationData.Yaw = packet.Demo.Psi / 1000;
                navigationData.Altitude = new Distance(packet.Demo.Altitude / 1000.0f);

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
                navigationData.Magneto.Raw.X = packet.Magneto.MagnetoRaw.X;
                navigationData.Magneto.Raw.Y = packet.Magneto.MagnetoRaw.Y;
                navigationData.Magneto.Raw.Z = packet.Magneto.MagnetoRaw.Z;

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

            // Wifi.
            if (packet.Wifi != null)
            {
                navigationData.Communication.Type = CommunicationType.Wifi;
                navigationData.Communication.LinkQuality = 1.0f - packet.Wifi.LinkQuality.ToFloat();
            }

            // Pwn.
            if (packet.Pwn != null)
            {
                if (packet.Pwn.SatMotor1 < 255)
                {
                    navigationData.FrontLeftEngine.IsFunctional = false;

                }
                if (packet.Pwn.SatMotor2 < 255)
                {
                    navigationData.FrontRightEngine.IsFunctional = false;

                }
                if (packet.Pwn.SatMotor3 < 255)
                {
                    navigationData.RearRightEngine.IsFunctional = false;

                }
                if (packet.Pwn.SatMotor4 < 255)
                {
                    navigationData.RearLeftEngine.IsFunctional = false;

                }
                //Debug.WriteLine(packet.Pwn);
                
                // Wind.
                if (packet.Wind != null)
                {
                    navigationData.Wind.Speed = new Speed(packet.Wind.WindSpeed, packet.Wind.WindSpeed, 0);
                    navigationData.Wind.Angle = Math.Round(packet.Wind.WindAngle) * -1;
                }
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
                Status = DroneStatus.Flying;
            }
            else
            {
                State |= NavigationState.Landed;
                Status = DroneStatus.Landed;
            }

            if (droneState.HasFlag(DroneStateMask.WindMask))
            {
                State |= NavigationState.Wind;
            }

            if (droneState.HasFlag(DroneStateMask.EmergencyMask))
            {
                State |= NavigationState.Emergency;
                Status = DroneStatus.Emergency;
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
