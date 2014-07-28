﻿using Drones.ARDrone.Client.Navdata.Native.Math;
using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_magneto_t
    {
        public ushort tag;
        public ushort size;

        public short mx;
        public short my;
        public short mz;

        /// <summary> Magneto in the body frame, in mG. </summary>
        public vector31_t magneto_raw;

        public vector31_t magneto_rectified;
        public vector31_t magneto_offset;
        public float heading_unwrapped;
        public float heading_gyro_unwrapped;
        public float heading_fusion_unwrapped;
        public sbyte magneto_calibration_ok;
        public uint magneto_state;
        public float magneto_radius;
        public float error_mean;
        public float error_var;
    }
}
