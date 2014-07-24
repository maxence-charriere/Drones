using Drones.Infrastructure;
using System;
using System.IO;

namespace Drones.ARDrone.Client.Navdata.Blocks
{
    public class NavdataHeader : INativeBlock
    {
        // @Properties
        public uint Header { get; private set; }
        public uint DroneState { get; private set; }
        public uint Sequence { get; private set; }
        public bool IsVisionDefined { get; private set; }
        public bool IsValid { get; private set; }

        public ushort Size
        {
            get
            {
                return _size;
            }
        }


        // @Public
        public NavdataHeader()
        {
        }

        public static NavdataHeader FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataHeader();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Header = reader.ReadUInt32();
                    block.DroneState = reader.ReadUInt32();
                    block.Sequence = reader.ReadUInt32();
                    block.IsVisionDefined = Convert.ToBoolean(reader.ReadInt32());
                    block.IsValid = block.Header == _navdataHeader;
                }
            }
            return block;
        }


        // @Private
        const int _navdataHeader = 0x55667788;
        const ushort _size = (sizeof(uint) * 3) + sizeof(int);
    }
}
