using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Math
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct vector21_t
    {
        public float x;
        public float y;
    }
}