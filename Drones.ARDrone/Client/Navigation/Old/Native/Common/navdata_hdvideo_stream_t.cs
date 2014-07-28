using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_hdvideo_stream_t
    {
        public ushort tag;
        public ushort size;

        public uint hdvideo_state;
        public uint storage_fifo_nb_packets;
        public uint storage_fifo_size;

        /// <summary> USB key in kbytes - 0 if no key present. </summary>
        public uint usbkey_size;

        /// <summary> USB key free space in kbytes - 0 if no key present. </summary>
        public uint usbkey_freespace;

        /// <summary>
        /// 'frame_number' PaVE field of the frame starting to be encoded for the HD stream.
        /// </summary>
        public uint frame_number;

        /// <summary> Time in seconds. </summary>
        public uint usbkey_remaining_time;
    }
}
