using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary>
    /// Controls the basic behaviour of the drone (take-off/landing, emergency stop/reset).
    /// </summary>
    public class RefCommand : ATCommand
    {
        // @Public
        public static readonly RefCommand Land = new RefCommand(RefMode.Land);
        public static readonly RefCommand TakeOff = new RefCommand(RefMode.TakeOff);
        public static readonly RefCommand Emergency = new RefCommand(RefMode.Emergency);
        public readonly RefMode Mode;

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*REF={0},{1}\r", sequenceNumber, (int)Mode);
        }


        // @Private
        RefCommand(RefMode mode)
        {
            Mode = mode;
        }
    }
}
