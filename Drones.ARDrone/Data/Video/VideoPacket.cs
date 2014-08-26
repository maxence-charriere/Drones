using System;
using System.Runtime.InteropServices;

namespace Drones.ARDrone.Data.Video
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VideoPacket
    {
        // @Properties
        public long Timestamp;
        public uint FrameNumber;
        public ushort Height;
        public ushort Width;
        public VideoFrameType FrameType;
        public byte[] Data;

        public static VideoPacket FromParrotVideoEncapsulation(ParrotVideoEncapsulation pve)
        {
            var packet = new VideoPacket()
            {
                Timestamp = DateTime.UtcNow.Ticks,
                FrameNumber = pve.FrameNumber,
                Width = pve.DisplayWidth,
                Height = pve.DisplayHeight,
                Data = new byte[pve.PayloadSize]
            };

            switch (pve.FrameType)
            {
                case ParrotVideoEncapsulationFrameTypes.IdrFrame:
                case ParrotVideoEncapsulationFrameTypes.IFrame:
                    packet.FrameType = VideoFrameType.I;
                    break;
                case ParrotVideoEncapsulationFrameTypes.PFrame:
                    packet.FrameType = VideoFrameType.P;
                    break;
                default:
                    packet.FrameType = VideoFrameType.Unknown;
                    break;
            }

            return packet;
        }

        public override string ToString()
        {
            return string.Format("VideoPacket (Timestamp: {0}, FrameNumber: {1}, Width: {2}, Height: {3}, FrameType: {4}, DataLength: {5})",
                Timestamp,
                FrameNumber,
                Width,
                Height,
                FrameType,
                Data.Length);
        }
    }
}
