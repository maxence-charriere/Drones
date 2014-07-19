using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary>
    /// This command asks the drone to calibrate the drone magnetometer.
    /// 
    /// This command MUST be sent when the AR.Drone is ﬂying. When receiving this command, the
    /// drone will automatically calibrate its magnetometer by spinning around itself for a few
    /// time.
    /// </summary>  
    public class CalibCommand : ATCommand
    {
        // @Properties
        public readonly CalibrationDevice Device;

        public static readonly CalibCommand Magnetometer = new CalibCommand(CalibrationDevice.Magnetometer);


        // @Public
        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*CALIB={0},{1}\r",
                sequenceNumber,
                (int)Device);
        }


        // @Private
        CalibCommand(CalibrationDevice device)
        {
            Device = device;
        }
    }
}
