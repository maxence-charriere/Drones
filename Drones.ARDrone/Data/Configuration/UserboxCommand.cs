using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Data.Configuration
{
    public struct UserboxCommand
    {
        // @Properties
        public UserboxCommandType Type { get; private set; }

        /// <summary> Gets or sets the delay between screenshots (in seconds). </summary>
        ///
        /// <value> The delay. </value>
        public int Delay { get; private set; }

        /// <summary> Gets or sets the number of screenshot to take. </summary>
        ///
        /// <value> The total number of screenshot to take. </value>
        public int Number { get; private set; }

        public DateTime Timestamp { get; private set; }


        // @Public
        public UserboxCommand(UserboxCommandType type, DateTime timestamp)
            : this()
        {
            Type = type;
            Timestamp = timestamp;
        }

        public UserboxCommand(UserboxCommandType type)
            : this()
        {
            Type = type;
            Timestamp = DateTime.Now;
        }

        public static UserboxCommand Parse(string value)
        {
            string[] parts = value.Split(',');
            var command = new UserboxCommand();
            var type = UserboxCommandType.Stop;
            if (parts.Length > 0 && Enum.TryParse(parts[0], out type))
            {
                command.Type = type;
            }

            DateTime timestamp;
            switch (type)
            {
                case UserboxCommandType.Start:
                    if (parts.Length > 1)
                    {
                        if (TryParseDate(parts[1], out timestamp))
                        {
                            command.Timestamp = timestamp;
                        }
                    }
                    break;
                case UserboxCommandType.Screenshot:
                    if (parts.Length > 3)
                    {
                        int delay;
                        int number;

                        if (int.TryParse(parts[1], out delay))
                        {
                            command.Delay = delay;
                        }
                        if (int.TryParse(parts[2], out number))
                        {
                            command.Number = number;
                        }
                        if (TryParseDate(parts[3], out timestamp))
                        {
                            command.Timestamp = timestamp;
                        }
                    }
                    break;
                default:
                    break;
            }
            return command;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case UserboxCommandType.Screenshot:
                    return string.Format("{0},{1},{2},{3}", (int)Type, Delay, Number, Timestamp.ToString(_dateTimeFormat));
                case UserboxCommandType.Start:
                    return string.Format("{0},{1}", (int)Type, Timestamp.ToString(_dateTimeFormat));
                default:
                    return string.Format("{0}", (int)Type);
            }
        }


        // @Private
        const string _dateTimeFormat = "yyyyMMdd_HHmmss";

        static bool TryParseDate(string s, out DateTime timestamp)
        {
            return DateTime.TryParseExact(s, _dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out timestamp);
        }
    }
}
