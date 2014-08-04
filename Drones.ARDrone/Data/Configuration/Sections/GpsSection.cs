using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class GpsSection : SectionBase
    {
        // @Properties
        public double Latitude
        {
            get { return GetDouble("latitude"); }
        }

        public double Longitude
        {
            get { return GetDouble("longitude"); }
        }

        public double Altitude
        {
            get { return GetDouble("altitude"); }
        }


        // @Public
        public GpsSection(Config config)
            : base(config, "gps")
        {
        }
    }
}
