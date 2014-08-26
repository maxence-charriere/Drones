using Drones.ARDrone.Data.Video;
using Drones.ARDrone.Extensions;
using Drones.Client.Video;
using Drones.Infrastructure;
using FFmpeg.AutoGen;

namespace Drones.ARDrone.Client.Video
{
    public class VideoPacketDecoder : DisposableBase
    {
        // @Public
        public VideoPacketDecoder(VideoPixelFormat pixelFormat)
        {
            _pixelFormat = pixelFormat;
            _avFrame = new AVFrame();
            _avPacket = new AVPacket();
        }

        public unsafe bool TryDecode(ref VideoPacket packet, out VideoFrame frame)
        {
            if (_videoDecoder == null)
            {
                _videoDecoder = new VideoDecoder();
            }


            fixed (byte* pData = &packet.Data[0])
            {
                _avPacket.data = pData;
                _avPacket.size = packet.Data.Length;
                if (_videoDecoder.TryDecode(ref _avPacket, ref _avFrame))
                {
                    if (_videoConverter == null)
                    {
                        _videoConverter = new VideoConverter(_pixelFormat.ToAVPixelFormat());
                    }

                    byte[] data = _videoConverter.ConvertFrame(ref _avFrame);

                    frame = new VideoFrame();
                    frame.Timestamp = packet.Timestamp;
                    frame.Number = packet.FrameNumber;
                    frame.Width = packet.Width;
                    frame.Height = packet.Height;
                    frame.Depth = data.Length / (packet.Width * packet.Height);
                    frame.PixelFormat = _pixelFormat;
                    frame.Data = data;
                    return true;
                }
            }
            frame = null;
            return false;
        }


        // @Protected
        protected override void DisposeOverride()
        {
            if (_videoDecoder != null)
            {
                _videoDecoder.Dispose();
            }
            if (_videoConverter != null)
            {
                _videoConverter.Dispose();
            }
        }

        // @Private
        readonly VideoPixelFormat _pixelFormat;
        VideoConverter _videoConverter;
        VideoDecoder _videoDecoder;
        AVFrame _avFrame;
        AVPacket _avPacket;

    }


}
