using Drones.Client.Video;
using FFmpeg.AutoGen;
using System;

namespace Drones.ARDrone.Extensions
{
    public static class PixelFormatExtensions
    {
        public static AVPixelFormat ToAVPixelFormat(this VideoPixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case VideoPixelFormat.Gray8:
                    return AVPixelFormat.PIX_FMT_GRAY8;
                case VideoPixelFormat.BGR24:
                    return AVPixelFormat.PIX_FMT_BGR24;
                case VideoPixelFormat.RGB24:
                    return AVPixelFormat.PIX_FMT_RGB24;
                default:
                    throw new ArgumentOutOfRangeException("pixelFormat");
            }
        }
    }
}
