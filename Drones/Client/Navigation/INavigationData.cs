using System;

namespace Drones.Client.Navigation
{
    public interface INavigationData
    {
        // @Properties
        double Pitch { get; set; }
        double Roll { get; set; }
        double Yaw { get; set; }

        /// <summary> Altitude in metres. </summary>
        ///
        /// <value> The altitude. </value>
        Distance Altitude { get; set; }

        DateTime Time { get; set; }
        Speed Speed { get; set; }
        Magneto Magneto { get; set; }
        Battery Battery { get; set; }
        Communication Communication { get; set; }
        Video Video { get; set; }
    }
}
