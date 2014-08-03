using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class SyslogSection : SectionBase
    {
        // @Properties
        public int Output
        {
            get { return GetInt("output"); }
            set { Set("output", value); }
        }

        public int MaxSize
        {
            get { return GetInt("max_size"); }
            set { Set("max_size", value); }
        }

        public int NbFiles
        {
            get { return GetInt("nb_files"); }
            set { Set("nb_files", value); }
        }


        // @Public
        public SyslogSection(Configuration config)
            : base(config, "syslog")
        {
        }
    }
}
