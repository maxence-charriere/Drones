using Drones.ARDrone.Client.Navdata.Native.Math;
using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary> Minimal navigation data for all flights. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct navdata_demo_t
    {
        /// <summary> Navdata block ('option') identifier. </summary>
        public ushort tag;

        /// <summary> Set this to the size of this structure. </summary>
        public ushort size;

        /// <summary>
        /// Flying state (landed, flying, hovering, etc.) defined in CTRL_STATES enum.
        /// </summary>
        public uint ctrl_state;

        /// <summary> Battery voltage filtered (mV). </summary>
        public uint vbat_flying_percentage;

        /// <summary> UAV's pitch (in milli-degree). </summary>
        public float theta;

        /// <summary> UAV's roll (in milli-degree). </summary>
        public float phi;

        /// <summary> UAV's yaw (in milli-degree). </summary>
        public float psi;

        /// <summary> UAV's altitude in centimeters. </summary>
        public int altitude;

        /// <summary> UAV's estimated linear velocity. </summary>
        public float vx;

        /// <summary> UAV's estimated linear velocity. </summary>
        public float vy;

        /// <summary> UAV's estimated linear velocity. </summary>
        public float vz;

        /// <summary> Streamed frame index. Not used -> To integrate in video stage. </summary>
        public uint num_frames;

        /// <summary> Deprecated. </summary>
        public matrix33_t detection_camera_rot;

        /// <summary> Deprecated. </summary>
        public vector31_t detection_camera_trans;

        /// <summary> Deprecated. </summary>
        public uint detection_tag_index;

        /// <summary> Type of tag searched in detection. </summary>
        public uint detection_camera_type;

        /// <summary> Deprecated. </summary>
        public matrix33_t drone_camera_rot;

        /// <summary> Deprecated. </summary>
        public matrix33_t drone_camera_trans;
    }
}
