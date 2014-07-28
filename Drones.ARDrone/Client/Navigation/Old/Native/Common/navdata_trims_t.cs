using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_trims_t
    {
        public ushort tag;
        public ushort size;

        public float angular_rates_trim_r;
        public float euler_angles_trim_theta;
        public float euler_angles_trim_phi;
    }
}
