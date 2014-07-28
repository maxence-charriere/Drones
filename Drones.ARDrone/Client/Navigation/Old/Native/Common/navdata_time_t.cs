using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary> Timestamp + 6 bytes. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_time_t
    {
        public ushort tag;
        public ushort size;

        /// <summary>
        /// 32 bit value where the 11 most significant bits represents the seconds, and the 21
        /// least significant bits are the microseconds.
        /// </summary>
        public uint time;
    }
}
