using Drones.ARDrone.Client.Navdata.Native.Screen;
using Drones.ARDrone.Client.Navdata.Native.Vision;
using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct navdata_trackers_send_t
    {
        public ushort tag;
        public ushort size;

        public fixed int locked[def_vision_common.DEFAULT_NB_TRACKERS_WIDTH * def_vision_common.DEFAULT_NB_TRACKERS_HEIGHT];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = def_vision_common.DEFAULT_NB_TRACKERS_WIDTH * def_vision_common.DEFAULT_NB_TRACKERS_HEIGHT)]
        public screen_point_t[] point;
    }
}
