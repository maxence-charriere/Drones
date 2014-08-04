using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class UserboxSection : SectionBase
    {
        // @Properties
        public UserboxCommand Command
        {
            get { return GetUserboxCommand("userbox_cmd"); }
            set { Set("userbox_cmd", value); }
        }


        // @Public
        public UserboxSection(Config config)
            : base(config, "userbox")
        {
        }
    }
}
