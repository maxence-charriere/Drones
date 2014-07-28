using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary> Navdata structure sent over the network. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_t
    {
        /// <summary> Always set to NAVDATA_HEADER. </summary>
        public uint header;

        /// <summary> Bit mask built from def_ardrone_state_mask_t. </summary>
        public uint ardrone_state;

        /// <summary> Sequence number, incremented for each sent packet. </summary>
        public uint sequence;

        public int vision_defined;
    }
}
