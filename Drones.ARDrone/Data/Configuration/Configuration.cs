using Drones.ARDrone.Data.Configuration.Sections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Drones.ARDrone.Data.Configuration
{
    public class Configuration
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

        public Configuration()
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

        public static Configuration Parse(string input)
        {
            var configuration = new Configuration();

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
