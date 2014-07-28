using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary> Deprecated. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_zimmu_3000_t
    {
        public ushort tag;
        public ushort size;

        public int vzimmuLSB;
        public float vzfind;
    }
}
