using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct navdata_phys_measures_t
    {
        public ushort tag;
        public ushort size;

        public float accs_temp;
        public ushort gyro_temp;
        public fixed float phys_accs[(int)def_acc_t.NB_ACCS];
        public fixed float phys_gyros[(int)def_gyro_t.NB_GYROS];

        /// <summary> 3.3volt alim [LSB]. </summary>
        public uint alim3V3;

        /// <summary> Ref volt Epson gyro [LSB]. </summary>
        public uint vrefEpson;

        /// <summary> Ref volt IDG gyro [LSB]. </summary>
        public uint vrefIDG;
    }
}
