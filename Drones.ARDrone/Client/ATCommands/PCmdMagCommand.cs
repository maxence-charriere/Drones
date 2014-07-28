using Drones.ARDrone.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary> This command controls the drone ﬂight motions. </summary>
    public class PCmdMagCommand : PCmdCommand
    {
        // @Public
        public readonly float Psi;
        public readonly float Accuracy;

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
        /// <param name="psi">        The psi. Magneto psi - ﬂoating-point value in range [−1..1]. </param>
        /// <param name="accuracy">   The accuracy. Magneto psi accuracy - ﬂoating-point value in
        ///                           range [−1..1]. </param>
        public PCmdMagCommand(FlightMode flightMode, float roll, float pitch, float gaz, float yaw, float psi, float accuracy) 
            : base(flightMode, roll, pitch, gaz, yaw)
        {
            Psi = psi;
            Accuracy = accuracy;
        }

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*PCMD_MAG={0},{1},{2},{3},{4},{5},{6},{7}\r",
                sequenceNumber,
                (int)FlightMode,
                Roll.ToInt(),
                Pitch.ToInt(),
                Gaz.ToInt(),
                Yaw.ToInt(),
                Psi.ToInt(),
                Accuracy.ToInt());
        }
    }
}
