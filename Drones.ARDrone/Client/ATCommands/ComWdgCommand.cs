using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary> Reset communication watchdog. </summary>
    public class ComWdgCommand : ATCommand
    {
        // @Properties
        public static readonly ComWdgCommand Default = new ComWdgCommand();
        

        // @Public
        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*COMWDG={0}\r", sequenceNumber);
        }


        // @Private
        ComWdgCommand()
        {
        }
    }
}
