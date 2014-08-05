using System;

namespace Drones.Client.Navigation
{
    public interface INavigationData
    {
        // @Properties
        /// <summary> Gets or sets the pitch angle (in degree). </summary>
        ///
        /// <value> The pitch. </value>
        double Pitch { get; set; }

        /// <summary> Gets or sets the roll angle (in degree). </summary>
        ///
        /// <value> The roll. </value>
        double Roll { get; set; }

        /// <summary> Gets or sets the yaw angle (in degree). </summary>
        ///
        /// <value> The yaw. </value>
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
