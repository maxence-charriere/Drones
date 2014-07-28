using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Screen
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct screen_point_t
    {
        public short x;
        public short y;
    }
}
