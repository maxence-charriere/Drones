using System;
using System.Collections.Generic;
using System.Globalization;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class SectionBase
    {
        // @Public
        public SectionBase(Config config, string name)
        {
            _config = config;
            _name = name;
        }


        // @Protected
        protected string GetString(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return value;
            }
            return default(string);
        }

        protected int GetInt(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return int.Parse(value);
            }
            return default(int);
        }

        protected float GetFloat(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return float.Parse(value, CultureInfo.InvariantCulture);
            }
            return default(float);
        }

        protected double GetDouble(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return double.Parse(value, CultureInfo.InvariantCulture);
            }
            return default(double);
        }

        protected bool GetBool(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return bool.Parse(value);
            }
            return default(bool);
        }

        protected FlightAnimation GetFlightAnimation(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return FlightAnimation.Parse(value);
            }
            return default(FlightAnimation);
        }

        protected LedAnimation GetLedAnimation(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return LedAnimation.Parse(value);
            }
            return default(LedAnimation);
        }

        protected UserboxCommand GetUserboxCommand(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return UserboxCommand.Parse(value);
            }
            return default(UserboxCommand);
        }

        protected T GetEnum<T>(string key)
        {
            string value;
            if (_config.Items.TryGetValue(GetFullKey(key), out value))
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            return default(T);
        }

        protected void Set(string key, string value)
        {
            key = GetFullKey(key);
            if (_config.Items.ContainsKey(key) == false)
            {
                _config.Items.Add(key, value);
            }
            else
            {
                _config.Items[key] = value;
            }
            _config.Changes.Enqueue(new KeyValuePair<string, string>(key, value));
        }

        protected void Set(string key, int value)
        {
            Set(key, value.ToString("D"));
        }

        protected void Set(string key, float value)
        {
            Set(key, value.ToString(CultureInfo.InvariantCulture));
        }

        protected void Set(string key, double value)
        {
            Set(key, Convert.ToSingle(value).ToString(CultureInfo.InvariantCulture));
        }

        protected void Set(string key, bool value)
        {
            Set(key, value.ToString().ToUpper());
        }

        protected void Set(string key, FlightAnimation value)
        {
            Set(key, value.ToString());
        }

        protected void Set(string key, LedAnimation value)
        {
            Set(key, value.ToString());
        }

        protected void Set(string key, UserboxCommand value)
        {
            Set(key, value.ToString());
        }

        protected void SetEnum<T>(string key, Enum value)
        {
            Set(key, value.ToString("D"));
        }


        // @Private
        readonly Config _config;
        readonly string _name;

        string GetFullKey(string s)
        {
            return _name + ":" + s;
        }
    }
}
