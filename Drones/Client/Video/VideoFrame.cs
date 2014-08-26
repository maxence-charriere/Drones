using System;

namespace Drones.Client.Video
{
    public class VideoFrame
    {
        // @Properties
        public long Timestamp { get; set; }
        public uint Number { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public VideoPixelFormat PixelFormat { get; set; }
        public byte[] Data { get; set; }
    }
}
