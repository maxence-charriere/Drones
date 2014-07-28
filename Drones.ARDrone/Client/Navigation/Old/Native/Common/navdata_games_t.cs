﻿using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_games_t
    {
        public ushort tag;
        public ushort size;

        public uint double_tap_counter;
        public uint finish_line_counter;
    }
}
