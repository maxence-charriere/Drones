using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary>
    /// This command sets a reference of the horizontal plane for the drone internal control
    /// system.
    /// 
    /// It must be called after each drone start up, while making sure the drone actually sits on
    /// a horizontal ground. Not doing so before taking-off will result in the drone not being
    /// able to stabilizeitselfwhenﬂying,asitwouldnotbeabletoknowitsactualtilt. ThiscommandMUST
    /// NOT be sent when the AR.Drone is ﬂying.
    /// 
    /// When receiving this command, the drone will automatically adjust the trim on pitch and
    /// roll controls.
    /// </summary>
    public class FTrimCommand : ATCommand
    {
        // @Public
        public static readonly FTrimCommand Default = new FTrimCommand();

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*FTRIM={0}\r", sequenceNumber);
        }  


        // @Private
        FTrimCommand()
        {
        }
    }
}
