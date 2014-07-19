using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    /// <summary> Sets an conﬁgurable option on the drone. </summary>
    public class ConfigCommand : ATCommand
    {
        // @Command
        public readonly string Name;
        public readonly string Value;


        // @Public
        /// <summary> Constructor. </summary>
        ///
        /// <param name="name">  The name of the option to set. </param>
        /// <param name="value"> The option value. </param>
        public ConfigCommand(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString(int sequenceNumber)
        {
            return string.Format("AT*CONFIG={0},\"{1}\",\"{2}\"\r",
                sequenceNumber,
                Name,
                Value);
        }
    }
}
