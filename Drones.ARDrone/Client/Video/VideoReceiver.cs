using Drones.ARDrone.Data.Video;
using Drones.Infrastructure;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Drones.ARDrone.Client.Video
{
    public class VideoReceiver : WorkerBase
    {
        // @Events
        public event Action<VideoPacket> VideoPacketAcquired;


        // @Public
        public const int VideoPort = 5555;
        public const int FrameBufferSize = 0x100000;
        public const int NetworkStreamReadSize = 0x1000;
        public readonly string Hostname;

        public VideoReceiver(string hostname)
        {
            Hostname = hostname;
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine("VideoReceiver started.");
            using (var tcpClient = new TcpClient(Hostname, VideoPort))
            {
                using (var networkStream = tcpClient.GetStream())
                {
                    var buffer = new byte[FrameBufferSize];
                    int position = 0;
                    int offset = 0;
                    VideoPacket? currentPacket = null;

                    while (token.IsCancellationRequested == false)
                    {
                        // Packet case.
                        if (currentPacket == null && (offset - position) >= ParrotVideoEncapsulation.BlockSize)
                        {
                            ParrotVideoEncapsulation pve = null;
                            if (ParrotVideoEncapsulation.TryParse(out pve, buffer, (uint)position))
                            {
                                currentPacket = VideoPacket.FromParrotVideoEncapsulation(pve);
                                position += pve.HeaderSize;
                            }
                            else
                            {
                                ++position;
                            }
                        }

                        // Frame case.
                        else if (currentPacket != null && (offset - position) >= currentPacket.Value.Data.Length)
                        {
                            Array.Copy(buffer, position, currentPacket.Value.Data, 0, currentPacket.Value.Data.Length);
                            position += currentPacket.Value.Data.Length;
                            RaiseVideoPacketAcquired(currentPacket.Value);
                            currentPacket = null;

                            // Clean buffer.
                            var dataLength = offset - position;
                            Array.Copy(buffer, position, buffer, 0, dataLength);
                            position = 0;
                            offset = dataLength;
                        }


                        // Read Data.
                        else if (tcpClient.Available > 0)
                        {
                            offset += networkStream.Read(buffer, offset, NetworkStreamReadSize);
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("VideoReceiver stopped.");
        }


        // @Private
        void RaiseVideoPacketAcquired(VideoPacket packet)
        {
            if (VideoPacketAcquired != null)
            {
                VideoPacketAcquired(packet);
                //System.Diagnostics.Debug.WriteLine(packet);
            }
        }
    }
}
