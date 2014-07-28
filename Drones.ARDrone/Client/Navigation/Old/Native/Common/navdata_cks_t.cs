using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary>
    /// Last navdata option that *must* be included at the end of all navdata packets
    /// + 6 bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_cks_t
    {
        public ushort tag;
        public ushort size;

        /// <summary> Checksum for all navdatas (including options). </summary>
        public uint cks;
    }
}
