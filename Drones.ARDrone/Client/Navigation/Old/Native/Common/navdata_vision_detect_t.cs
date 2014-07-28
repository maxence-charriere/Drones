using Drones.ARDrone.Client.Navdata.Native.Math;
using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct navdata_vision_detect_t
    {
        public ushort tag;
        public ushort size;

        public uint nb_detected;
        public fixed uint type[def_common.NB_NAVDATA_DETECTION_RESULTS];
        public fixed uint xc[def_common.NB_NAVDATA_DETECTION_RESULTS];
        public fixed uint yc[def_common.NB_NAVDATA_DETECTION_RESULTS];
        public fixed uint width[def_common.NB_NAVDATA_DETECTION_RESULTS];
        public fixed uint height[def_common.NB_NAVDATA_DETECTION_RESULTS];
        public fixed uint dist[def_common.NB_NAVDATA_DETECTION_RESULTS];
        public fixed float orientation_angle[def_common.NB_NAVDATA_DETECTION_RESULTS];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = def_common.NB_NAVDATA_DETECTION_RESULTS)]
        public matrix33_t[] rotation;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = def_common.NB_NAVDATA_DETECTION_RESULTS)]
        public vector31_t[] translation;

        public fixed uint camera_source[def_common.NB_NAVDATA_DETECTION_RESULTS];
    }
}
