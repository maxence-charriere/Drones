using System.Runtime.InteropServices;

namespace Drones.ARDrone.Client.Navdata.Native.Common
{
    /// <summary> Raw sensors measurements. </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public unsafe struct navdata_raw_measures_t
    {
        public ushort tag;
        public ushort size;

        /// <summary> Filtered accelerometers. </summary>
        public fixed ushort raw_accs[(int)def_acc_t.NB_ACCS];

        /// <summary> Filtered gyrometers. </summary>
        public fixed short raw_gyros[(int)def_gyro_t.NB_GYROS];

        /// <summary> Gyrometers  x/y 110 deg/s </summary>
        public fixed short raw_gyros_110[2];

        /// <summary> Battery voltage raw (mV). </summary>
        public uint vbat_raw;

        public ushort us_debut_echo;
        public ushort us_fin_echo;
        public ushort us_association_echo;
        public ushort us_distance_echo;
        public ushort us_courbe_temps;
        public ushort us_courbe_valeur;
        public ushort us_courbe_ref;
        public ushort flag_echo_ini;
        public ushort nb_echo;
        public uint sum_echo;
        public int alt_temp_raw;
        public short gradient;
    }
}
