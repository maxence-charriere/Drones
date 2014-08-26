using System;

namespace Drones.ARDrone.Client.Video
{
    public class VideoConverterException : Exception
    {
        public VideoConverterException(string message)
            : base(message)
        {
        }
    }
}
