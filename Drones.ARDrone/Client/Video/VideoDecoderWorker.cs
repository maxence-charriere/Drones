using Drones.ARDrone.Data.Video;
using Drones.Client.Video;
using Drones.ARDrone.Extensions;
using Drones.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace Drones.ARDrone.Client.Video
{
    public class VideoDecoderWorker : WorkerBase
    {
        // @Events
        public event Action<VideoFrame> FrameDecoded;


        // @Public
        public readonly VideoPixelFormat PixelFormat;
        
        public VideoDecoderWorker(VideoPixelFormat pixelFormat)
        {
            PixelFormat = pixelFormat;
        }

        public void EnqueueVideoPacket(VideoPacket packet)
        {
            if (packet.FrameType == VideoFrameType.I && _packetQueue.Count > _skipFramesThreshold)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Skipping {0} frames.", _packetQueue.Count));
                _packetQueue.Flush();
            }
            _packetQueue.Enqueue(packet);
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine("VideoDecoderWorker started.");
            _packetQueue.Flush();

            using (var videoDecoder = new VideoPacketDecoder(PixelFormat))
            {
                while (token.IsCancellationRequested == false)
                {
                    VideoPacket packet;
                    if (_packetQueue.TryDequeue(out packet))
                    {
                        VideoFrame frame;
                        if (videoDecoder.TryDecode(ref packet, out frame))
                        {
                            RaiseFrameDecoded(frame);
                        }
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("VideoDecoderWorker stopped.");
        }


        // @Private
        readonly ConcurrentQueue<VideoPacket> _packetQueue = new ConcurrentQueue<VideoPacket>();
        const int _skipFramesThreshold = 1;

        void RaiseFrameDecoded(VideoFrame frame)
        {
            if (FrameDecoded != null)
            {
                FrameDecoded(frame);
            }
        }
    }
}
