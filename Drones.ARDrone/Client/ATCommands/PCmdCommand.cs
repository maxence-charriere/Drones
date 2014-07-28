using Drones.ARDrone.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary> This command controls the drone ﬂight motions. </summary>
    public class PCmdCommand : ATCommand
    {
        // @Public
        public readonly FlightMode FlightMode;
        public readonly float Roll;
        public readonly float Pitch;
        public readonly float Gaz;
        public readonly float Yaw;

        /// <summary> Constructor. </summary>
        ///
        /// <param name="flightMode"> The flight mode. </param>
        /// <param name="roll">       The roll. Drone left-right tilt - ﬂoating-point value in
        ///                           range [−1..1]. </param>
        /// <param name="pitch">      The pitch. Drone front-back tilt - ﬂoating-point value in
        ///                           range [−1..1]. </param>
        /// <param name="gaz">        The gaz. Drone vertical speed - ﬂoating-point value in range
        ///                           [−1..1]. </param>
        /// <param name="yaw">        The yaw. Drone angular speed - ﬂoating-point value in range
        ///                           [−1..1]. </param>
        public PCmdCommand(FlightMode flightMode, float roll, float pitch, float gaz, float yaw)
        {
            FlightMode = flightMode;
            Roll = roll;
            Pitch = pitch;
            Gaz = gaz;
            Yaw = yaw;
        }

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*PCMD={0},{1},{2},{3},{4},{5}\r",
                sequenceNumber,
                (int)FlightMode,
                Roll.ToInt(),
                Pitch.ToInt(),
                Gaz.ToInt(),
                Yaw.ToInt());
        }
    }
}
