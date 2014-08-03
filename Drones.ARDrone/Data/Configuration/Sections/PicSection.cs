using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class PicSection : SectionBase
    {
        // @Properties
        public int UltrasoundFreq
        {
            get { return GetInt("ultrasound_freq"); }
            set { Set("ultrasound_freq", value); }
        }

        public int UltrasoundWatchdog
        {
            get { return GetInt("ultrasound_watchdog"); }
            set { Set("ultrasound_watchdog", value); }
        }

        public int Version
        {
            get { return GetInt("pic_version"); }
        }


        // @Public
        public PicSection(Configuration config)
            : base(config, "pic")
        {
        }
    }
}
