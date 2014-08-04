using Drones.ARDrone.Data.Configuration.Sections;
using Drones.Client.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Drones.ARDrone.Data.Configuration
{
    public class Config
    {
        // @Public
        public Dictionary<string, string> Items;
        public readonly ConcurrentQueue<KeyValuePair<string, string>> Changes;
        public readonly GeneralSection General;
        public readonly ControlSection Control;
        public readonly NetworkSection Network;
        public readonly PicSection Pic;
        public readonly VideoSection Video;
        public readonly LedsSection Leds;
        public readonly DetectSection Detect;
        public readonly SyslogSection Syslog;
        public readonly UserboxSection Userbox;
        public readonly GpsSection Gps;
        public readonly CustomSection Custom;

        public Config()
        {
            Items = new Dictionary<string, string>();
            Changes = new ConcurrentQueue<KeyValuePair<string, string>>();

            General = new GeneralSection(this);
            Control = new ControlSection(this);
            Network = new NetworkSection(this);
            Pic = new PicSection(this);
            Video = new VideoSection(this);
            Leds = new LedsSection(this);
            Detect = new DetectSection(this);
            Syslog = new SyslogSection(this);
            Userbox = new UserboxSection(this);
            Gps = new GpsSection(this);
            Custom = new CustomSection(this);
        }

        public void Update(Settings settings)
        {
            Control.ControlVzMax = (Convert.ToSingle(settings.VerticalThrust) * 2000) / 100;
            Control.EulerAngleMax = (Convert.ToSingle(settings.HorizontalThrust) * 0.52f) / 100;
            Control.ControlYaw = (Convert.ToSingle(settings.YawThrust) * 6.11f) / 100;
            Control.AltitudeMax = Convert.ToInt32(settings.MaximumAltitude * 1000);
            Control.FlightWithoutShell = settings.HullType == HullType.Outdoor ? true : false;
        }

        public static Config Parse(string input)
        {
            var configuration = new Config();

            MatchCollection matches = _regexKeyValue.Matches(input);
            foreach (Match match in matches)
            {
                string key = match.Groups["key"].Value;
                string value = match.Groups["value"].Value;
                configuration.Items.Add(key, value);
            }
            return configuration;
        }

        public static string NewId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }


        // @Private
        static readonly Regex _regexKeyValue = new Regex(@"(?<key>\w+:\w+) = (?<value>.*)");
    }
}
