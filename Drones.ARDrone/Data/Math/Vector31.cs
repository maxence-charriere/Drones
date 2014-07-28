using System.IO;

namespace Drones.ARDrone.Data.Math
{
    public class Vector31 : INativeBlock
    {
        // @Properties
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }

        public ushort Size
        {
            get
            {
                return DataSize;
            }
        }


        // @Public
        public const ushort DataSize = sizeof(float) * 3;

        public Vector31()
        {
        }

        public static Vector31 FromByteArray(byte[] data, uint position)
        {
            var block = new Vector31();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);
                    block.X = reader.ReadSingle();
                    block.Y = reader.ReadSingle();
                    block.Z = reader.ReadSingle();
                }
            }
            return block;
        }
    }
}
