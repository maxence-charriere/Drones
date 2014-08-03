using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class GeneralSection : SectionBase
    {
        // @Properties
        public int ConfigurationVersion
        {
            get { return GetInt("num_version_config"); }
        }

        public int HardwareVersion
        {
            get { return GetInt("num_version_mb"); }
        }

        public string FirmwareVersion
        {
            get { return GetString("num_version_soft"); }
        }

        public string DroneSerial
        {
            get { return GetString("drone_serial"); }
        }

        public string FirmwareBuildDate
        {
            get { return GetString("soft_build_date"); }
        }

        public string Motor1Soft
        {
            get { return GetString("motor1_soft"); }
        }

        public string Motor1Hard
        {
            get { return GetString("motor1_hard"); }
        }

        public string Motor1Supplier
        {
            get { return GetString("motor1_supplier"); }
        }

        public string Motor2Soft
        {
            get { return GetString("motor2_soft"); }
        }

        public string Motor2Hard
        {
            get { return GetString("motor2_hard"); }
        }

        public string Motor2Supplier
        {
            get { return GetString("motor2_supplier"); }
        }

        public string Motor3Soft
        {
            get { return GetString("motor3_soft"); }
        }

        public string Motor3Hard
        {
            get { return GetString("motor3_hard"); }
        }

        public string Motor3Supplier
        {
            get { return GetString("motor3_supplier"); }
        }

        public string Motor4Soft
        {
            get { return GetString("motor4_soft"); }
        }

        public string Motor4Hard
        {
            get { return GetString("motor4_hard"); }
        }

        public string Motor4Supplier
        {
            get { return GetString("motor4_supplier"); }
        }

        public string ARDroneName
        {
            get { return GetString("ardrone_name"); }
            set { Set("ardrone_name", value); }
        }

        public int FlyingTime
        {
            get { return GetInt("flying_time"); }
        }

        public bool NavdataDemo
        {
            get { return GetBool("navdata_demo"); }
            set { Set("navdata_demo", value); }
        }

        public NavdataOptionFlag NavdataOptions
        {
            get { return GetEnum<NavdataOptionFlag>("navdata_options"); }
            set { SetEnum<NavdataOptionFlag>("navdata_options", value); }
        }

        public int ComWatchdog
        {
            get { return GetInt("com_watchdog"); }
            set { Set("com_watchdog", value); }
        }

        public bool Video
        {
            get { return GetBool("video_enable"); }
            set { Set("video_enable", value); }
        }

        public bool Vision
        {
            get { return GetBool("vision_enable"); }
            set { Set("vision_enable", value); }
        }

        public int BatteryVoltageMin
        {
            get { return GetInt("vbat_min"); }
            set { Set("vbat_min", value); }
        }

        public int LocalTime
        {
            get { return GetInt("localtime"); }
            set { Set("localtime", value); }
        }


        // @Public
        public GeneralSection(Configuration config)
            : base(config, "general")
        {
        }
    }
}
