using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class ControlSection : SectionBase
    {
        // @Properties
        public string AccsOffset
        {
            get { return GetString("accs_offset"); }
        }

        public string AccsGains
        {
            get { return GetString("accs_gains"); }
        }

        public string GyrosOffset
        {
            get { return GetString("gyros_offset"); }
        }

        public string GyrosGains
        {
            get { return GetString("gyros_gains"); }
        }

        public string Gyros110Offset
        {
            get { return GetString("gyros110_offset"); }
        }

        public string Gyros110Gains
        {
            get { return GetString("gyros110_gains"); }
        }

        public string MagnetoOffset
        {
            get { return GetString("magneto_offset"); }
        }

        public float MagnetoRadius
        {
            get { return GetFloat("magneto_radius"); }
        }

        public float GyroOffsetThrX
        {
            get { return GetFloat("gyro_offset_thr_x"); }
        }

        public float GyroOffsetThrY
        {
            get { return GetFloat("gyro_offset_thr_y"); }
        }

        public float GyroOffsetThrZ
        {
            get { return GetFloat("gyro_offset_thr_z"); }
        }

        public int PwmRefGyros
        {
            get { return GetInt("pwm_ref_gyros"); }
        }

        public int OsctunValue
        {
            get { return GetInt("osctun_value"); }
        }

        public bool OsctunTest
        {
            get { return GetBool("osctun_test"); }
        }

        public int ControlLevel
        {
            get { return GetInt("control_level"); }
            set { Set("control_level", value); }
        }

        public float EulerAngleMax
        {
            get { return GetFloat("euler_angle_max"); }
            set { Set("euler_angle_max", value); }
        }

        public int AltitudeMax
        {
            get { return GetInt("altitude_max"); }
            set { Set("altitude_max", value); }
        }

        public int AltitudeMin
        {
            get { return GetInt("altitude_min"); }
            set { Set("altitude_min", value); }
        }

        public float ControliPhoneTilt
        {
            get { return GetFloat("control_iphone_tilt"); }
            set { Set("control_iphone_tilt", value); }
        }

        public float ControlVzMax
        {
            get { return GetFloat("control_vz_max"); }
            set { Set("control_vz_max", value); }
        }

        public float ControlYaw
        {
            get { return GetFloat("control_yaw"); }
            set { Set("control_yaw", value); }
        }

        public bool Outdoor
        {
            get { return GetBool("outdoor"); }
            set { Set("outdoor", value); }
        }

        public bool FlightWithoutShell
        {
            get { return GetBool("flight_without_shell"); }
            set { Set("flight_without_shell", value); }
        }

        public bool AutonomousFlight
        {
            get { return GetBool("autonomous_flight"); }
        }

        public bool ManualTrim
        {
            get { return GetBool("manual_trim"); }
            set { Set("manual_trim", value); }
        }

        public float IndoorEulerAngleMax
        {
            get { return GetFloat("indoor_euler_angle_max"); }
            set { Set("indoor_euler_angle_max", value); }
        }

        public float IndoorControlVzMax
        {
            get { return GetFloat("indoor_control_vz_max"); }
            set { Set("indoor_control_vz_max", value); }
        }

        public float IndoorControlYaw
        {
            get { return GetFloat("indoor_control_yaw"); }
            set { Set("indoor_control_yaw", value); }
        }

        public float OutdoorEulerAngleMax
        {
            get { return GetFloat("outdoor_euler_angle_max"); }
            set { Set("outdoor_euler_angle_max", value); }
        }

        public float OutdoorControlVzMax
        {
            get { return GetFloat("outdoor_control_vz_max"); }
            set { Set("outdoor_control_vz_max", value); }
        }

        public float OutdoorControlYaw
        {
            get { return GetFloat("outdoor_control_yaw"); }
            set { Set("outdoor_control_yaw", value); }
        }

        public int FlyingMode
        {
            get { return GetInt("flying_mode"); }
            set { Set("flying_mode", value); }
        }

        public int HoveringRange
        {
            get { return GetInt("hovering_range"); }
            set { Set("hovering_range", value); }
        }

        public FlightAnimation FlightAnimation
        {
            get { return GetFlightAnimation("flight_anim"); }
            set { Set("flight_anim", value); }
        }


        // @Public
        public ControlSection(Configuration config)
            : base(config, "control")
        {
        }
    }
}
