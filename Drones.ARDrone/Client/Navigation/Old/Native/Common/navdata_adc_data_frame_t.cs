﻿using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct navdata_adc_data_frame_t
    {
        public ushort tag;
        public ushort size;

        public uint version;
        public fixed byte data_frame[32];
    }
}
