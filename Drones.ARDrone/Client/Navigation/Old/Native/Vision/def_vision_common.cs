namespace Drones.ARDrone.Client.Navdata.Native.Vision
{
    public class def_vision_common
    {
        /// <summary> Number of trackers in width of current picture. </summary>
        public const int NB_CORNER_TRACKERS_WIDTH = 5;

        /// <summary> Number of trackers in height of current picture. </summary>
        public const int NB_CORNER_TRACKERS_HEIGHT = 4;

        public const int DEFAULT_NB_TRACKERS_WIDTH = NB_CORNER_TRACKERS_WIDTH + 1;
        public const int DEFAULT_NB_TRACKERS_HEIGHT = NB_CORNER_TRACKERS_HEIGHT + 1;

        public const int YBUF_OFFSET = 0;

        public const int DEFAULT_CS = 1;
        public const int DEFAULT_NB_PAIRS = 1;
        public const int DEFAULT_LOSS_PER = 1;
        public const int DEFAULT_SCALE = 1;
        public const int DEFAULT_TRANSLATION_MAX = 1;
        public const int DEFAULT_MAX_PAIR_DIST = 1;
        public const int DEFAULT_NOISE = 1;



    }
}
