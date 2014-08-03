using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class DetectSection : SectionBase
    {
        // @Properties
        public int EnemyColors
        {
            get { return GetInt("enemy_colors"); }
            set { Set("enemy_colors", value); }
        }

        public int GroundstripeColors
        {
            get { return GetInt("groundstripe_colors"); }
            set { Set("groundstripe_colors", value); }
        }

        public int EnemyWithoutShell
        {
            get { return GetInt("enemy_without_shell"); }
            set { Set("enemy_without_shell", value); }
        }

        public int Type
        {
            get { return GetInt("detect_type"); }
            set { Set("detect_type", value); }
        }

        public int DetectionsSelectH
        {
            get { return GetInt("detections_select_h"); }
            set { Set("detections_select_h", value); }
        }

        public int DetectionsSelectVHsync
        {
            get { return GetInt("detections_select_v_hsync"); }
            set { Set("detections_select_v_hsync", value); }
        }

        public int DetectionsSelectV
        {
            get { return GetInt("detections_select_v"); }
            set { Set("detections_select_v", value); }
        }


        // @Public
        public DetectSection(Configuration config)
            : base(config, "detect")
        {
        }
    }
}
