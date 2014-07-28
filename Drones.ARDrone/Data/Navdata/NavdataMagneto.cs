using Drones.ARDrone.Data.Math;
using System;
using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataMagneto : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public short MX { get; private set; }
        public short MY { get; private set; }
        public short MZ { get; private set; }
        public Vector31 MagnetoRaw { get; private set; }
        public Vector31 MagnetoRectified { get; private set; }
        public Vector31 MagnetoOffset { get; private set; }
        public float HeadingUnwrapped { get; private set; }
        public float HeadingGyroUnwrapped { get; private set; }
        public float HeadingFusionUnwrapped { get; private set; }
        public sbyte MagnetoCalibrationOk { get; private set; }
        public uint MagnetoState { get; private set; }
        public float MagnetoRadius { get; private set; }
        public float ErrorMean { get; private set; }
        public float ErrorVar { get; private set; }

        // @Public
        public NavdataMagneto()
        {
        }

        public static NavdataMagneto FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataMagneto();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.MX = reader.ReadInt16();
                    block.MY = reader.ReadInt16();
                    block.MZ = reader.ReadInt16();

                    block.MagnetoRaw = Vector31.FromByteArray(data, Convert.ToUInt32(ms.Position));
                    ms.Position += Vector31.DataSize;

                    block.MagnetoRectified = Vector31.FromByteArray(data, Convert.ToUInt32(ms.Position));
                    ms.Position += Vector31.DataSize;

                    block.MagnetoOffset = Vector31.FromByteArray(data, Convert.ToUInt32(ms.Position));
                    ms.Position += Vector31.DataSize;

                    block.MagnetoCalibrationOk = reader.ReadSByte();
                    block.MagnetoState = reader.ReadUInt32();
                    block.MagnetoRadius = reader.ReadSingle();
                    block.ErrorMean = reader.ReadSingle();
                    block.ErrorVar = reader.ReadSingle();
                }
            }
            return block;
        }
    }
}
