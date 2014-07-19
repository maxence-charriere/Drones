using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    public enum FlightMode
    {
        Hover = 0,
        Progressive = 1 << 0,
        CombinedYaw = 1 << 1,
        AbsoluteControl = 1 << 2
    }
}
