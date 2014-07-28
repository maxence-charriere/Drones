using Drones.ARDrone.Data;
using System.IO;

namespace Drones.ARDrone.Data.Math
{
    public class Matrix33 : INativeBlock
    {
        // @Properties
        public float M11 { get; private set; }
        public float M12 { get; private set; }
        public float M13 { get; private set; }
        public float M21 { get; private set; }
        public float M22 { get; private set; }
        public float M23 { get; private set; }
        public float M31 { get; private set; }
        public float M32 { get; private set; }
        public float M33 { get; private set; }

        public ushort Size
        {
            get
            {
                return DataSize;
            }
        }


        // @Public
        public const ushort DataSize = sizeof(float) * 9;

        public Matrix33()
        {
        }

        public static Matrix33 FromByteArray(byte[] data, uint position)
        {
            var block = new Matrix33();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);
                    block.M11 = reader.ReadSingle();
                    block.M12 = reader.ReadSingle();
                    block.M13 = reader.ReadSingle();
                    block.M21 = reader.ReadSingle();
                    block.M22 = reader.ReadSingle();
                    block.M23 = reader.ReadSingle();
                    block.M31 = reader.ReadSingle();
                    block.M32 = reader.ReadSingle();
                    block.M33 = reader.ReadSingle();
                }
            }
            return block;
        }
    }
}
