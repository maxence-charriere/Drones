using System;

namespace Drones.ARDrone.Data.Configuration.Sections
{
    public class VideoSection : SectionBase
    {
        // @Properties
        public int CamifFps
        {
            get { return GetInt("camif_fps"); }
        }

        public int CodecFps
        {
            get { return GetInt("codec_fps"); }
            set { Set("codec_fps", value); }
        }

        public int CamifBuffers
        {
            get { return GetInt("camif_buffers"); }
        }

        public int Trackers
        {
            get { return GetInt("num_trackers"); }
        }

        public VideoCodecType Codec
        {
            get { return GetEnum<VideoCodecType>("video_codec"); }
            set { SetEnum<VideoCodecType>("video_codec", value); }
        }

        public int Slices
        {
            get { return GetInt("video_slices"); }
            set { Set("video_slices", value); }
        }

        public int LiveSocket
        {
            get { return GetInt("video_live_socket"); }
            set { Set("video_live_socket", value); }
        }

        public int StorageSpace
        {
            get { return GetInt("video_storage_space"); }
        }

        public int Bitrate
        {
            get { return GetInt("bitrate"); }
            set { Set("bitrate", value); }
        }

        public int MaxBitrate
        {
            get { return GetInt("max_bitrate"); }
            set { Set("max_bitrate", value); }
        }

        public VideoBitrateControlMode BitrateCtrlMode
        {
            get { return GetEnum<VideoBitrateControlMode>("bitrate_ctrl_mode"); }
            set { SetEnum<VideoBitrateControlMode>("bitrate_ctrl_mode", value); }
        }

        public int BitrateStorage
        {
            get { return GetInt("bitrate_storage"); }
            set { Set("bitrate_storage", value); }
        }

        public VideoChannelType Channel
        {
            get { return GetEnum<VideoChannelType>("video_channel"); }
            set { SetEnum<VideoChannelType>("video_channel", value); }
        }

        public bool OnUsb
        {
            get { return GetBool("video_on_usb"); }
            set { Set("video_on_usb", value); }
        }

        public int FileIndex
        {
            get { return GetInt("video_file_index"); }
            set { Set("video_file_index", value); }
        }

        // @Public
        public VideoSection(Configuration config)
            : base(config, "video")
        {
        }
        
    }
}
