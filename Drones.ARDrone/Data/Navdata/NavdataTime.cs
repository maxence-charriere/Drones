using System;
using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataTime : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public uint Time { get; private set; }


        // @Public
        public NavdataTime()
        {
        }

        public static NavdataTime FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataTime();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.Time = reader.ReadUInt32();
                }
            }
            return block;
        }
    }
}
