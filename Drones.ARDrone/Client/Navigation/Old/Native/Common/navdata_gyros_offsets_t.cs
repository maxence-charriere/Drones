using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct navdata_gyros_offsets_t
    {
        public ushort tag;
        public ushort size;

        public fixed float offset_g[(int)def_gyro_t.NB_GYROS];
    }
}
