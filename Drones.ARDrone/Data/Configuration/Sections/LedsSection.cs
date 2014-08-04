using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class LedsSection : SectionBase
    {
        // @Properties
        public LedAnimation LedAnimation
        {
            get { return GetLedAnimation("leds_anim"); }
            set { Set("leds_anim", value); }
        }


        // @Public
        public LedsSection(Config config)
            : base(config, "leds")
        {
        }
    }
}
