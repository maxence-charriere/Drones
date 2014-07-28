using System;
using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataOption : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }    


        // @Public
        public NavdataOption()
        {
        }

        public static NavdataOption FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataOption();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                }
            }
            return block;
        }
    }
}
