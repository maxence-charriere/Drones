using System;

namespace Drones.ARDrone.Client.ATCommands
{
    [Flags]
    public enum FlightMode
    {
        Hover = 0,
        Progressive = 1 << 0,
        CombinedYaw = 1 << 1,
        AbsoluteControl = 1 << 2
    }
}
