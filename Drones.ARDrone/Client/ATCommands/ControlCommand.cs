using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    public class ControlCommand : ATCommand
    {
        // @Public
        public static ControlCommand AckControlMode = new ControlCommand(ControlMode.AckControlMode);
        public static ControlCommand CfgGetControlMode = new ControlCommand(ControlMode.CfgGetControlMode);
        public readonly ControlMode Mode;

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*CTRL={0},{1},0\r", sequenceNumber, (int)Mode);
        }


        // @Private
        ControlCommand(ControlMode mode)
        {
            Mode = mode;
        }
    }
}
