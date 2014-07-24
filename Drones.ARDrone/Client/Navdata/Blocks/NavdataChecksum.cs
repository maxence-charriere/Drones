using System;
using System.IO;

namespace Drones.ARDrone.Client.Navdata.Blocks
{
    public class NavdataChecksum : INativeBlock
    {
        // @Properties
        public static readonly NavdataChecksum Default = new NavdataChecksum();

        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public uint Checksum { get; private set; }


        // @Public
        public NavdataChecksum()
        {
        }

        public static NavdataChecksum FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataChecksum();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.Checksum = reader.ReadUInt32();
                }
            }
            return block;
        }
    }
}
