using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class CustomSection : SectionBase
    {
        // @Properties
        public string ApplicationId
        {
            get { return GetString("application_id"); }
            set { Set("application_id", value); }
        }

        public string ApplicationDescription
        {
            get { return GetString("application_desc"); }
            set { Set("application_desc", value); }
        }

        public string ProfileId
        {
            get { return GetString("profile_id"); }
            set { Set("profile_id", value); }
        }

        public string ProfileDescription
        {
            get { return GetString("profile_desc"); }
            set { Set("profile_desc", value); }
        }

        public string SessionId
        {
            get { return GetString("session_id"); }
            set { Set("session_id", value); }
        }

        public string SessionDescription
        {
            get { return GetString("session_desc"); }
            set { Set("session_desc", value); }
        }


        // @Public
        public CustomSection(Config config)
            : base(config, "custom")
        {
        }
    }
}
