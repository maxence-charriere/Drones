using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    public abstract class ATCommand
    {
        // @Public
        public abstract string ToString(int sequenceNumber);

        public byte[] ToByteArray(int sequenceNumber)
        {
            string ATCommandString = ToString(sequenceNumber);
            byte[] bytes = Encoding.ASCII.GetBytes(ATCommandString);
            return bytes;
        }
    }
}
