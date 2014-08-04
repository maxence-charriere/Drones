using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class NetworkSection : SectionBase
    {  
        // @Properties
        public string SsidSinglePlayer
        {
            get { return GetString("ssid_single_player"); }
            set { Set("ssid_single_player", value); }
        }

        public string SsidMultiPlayer
        {
            get { return GetString("ssid_multi_player"); }
            set { Set("ssid_multi_player", value); }
        }

        public int WifiMode
        {
            get { return GetInt("wifi_mode"); }
            set { Set("wifi_mode", value); }
        }

        public int WifiRate
        {
            get { return GetInt("wifi_rate"); }
            set { Set("wifi_rate", value); }
        }

        public string OwnerMac
        {
            get { return GetString("owner_mac"); }
            set { Set("owner_mac", value); }
        }


        // @Public
        public NetworkSection(Config config)
            : base(config, "network")
        {
        }
    }
}
