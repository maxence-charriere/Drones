using Drones.ARDrone.Extensions;
using System;

namespace Drones.ARDrone.Data.Configuration
{
    public struct LedAnimation
    {
        // @Properties
        public LedAnimationType Type { get; private set; }
        public float Frequency { get; private set; }
        public int Duration { get; private set; }


        // @Public
        public LedAnimation(LedAnimationType type, float frequency, int duration)
            : this()
        {
            Type = type;
            Frequency = frequency;
            Duration = duration;
        }

        public static LedAnimation Parse(string value)
        {
            string[] parts = value.Split(',');
            var animation = new LedAnimation();
            LedAnimationType type;
            int duration;
            int ifrequency;
            if (parts.Length > 2 && Enum.TryParse(parts[0], out type) && int.TryParse(parts[1], out ifrequency)
                && int.TryParse(parts[2], out duration))
            {
                animation.Type = type;
                animation.Frequency = ifrequency.ToFloat();
                animation.Duration = duration;
            }
            return animation;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", (int)Type, Frequency.ToInt(), Duration);
        }
    }
}
