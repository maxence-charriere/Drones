using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary> Velocities in float32_t format. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct velocities_t
    {
        public float x;
        public float y;
        public float z;
    }
}
