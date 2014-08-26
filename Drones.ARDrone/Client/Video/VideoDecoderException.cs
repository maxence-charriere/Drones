using System;

namespace Drones.ARDrone.Client.Video
{
    public class VideoDecoderException : Exception
    {
        public VideoDecoderException(string message)
            : base(message)
        {
        }
    }
}
