﻿using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataWifi : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public uint LinkQuality { get; private set; }    


        // @Public
        public NavdataWifi()
        {
        }

        public static NavdataWifi FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataWifi();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.LinkQuality = reader.ReadUInt32();
                }
            }
            return block;
        }
    }
}
