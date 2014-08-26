using System;
using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataWind : INativeBlock
    {
         // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }    
        public float WindSpeed { get; private set; }    
        public float WindAngle { get; private set; }    
        public float WindCompensationTheta { get; private set; }    
        public float WindCompensationPhi { get; private set; }    
        public float StateX1 { get; private set; }    
        public float StateX2 { get; private set; }    
        public float StateX3 { get; private set; }    
        public float StateX4 { get; private set; }    
        public float StateX5 { get; private set; }    
        public float StateX6 { get; private set; }    
        public float MagnetoDebug1 { get; private set; }    
        public float MagnetoDebug2 { get; private set; }    
        public float MagnetoDebug3 { get; private set; }    


        // @Public
        public NavdataWind()
        {
        }

        public static NavdataWind FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataWind();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.WindSpeed = reader.ReadSingle();
                    block.WindAngle = reader.ReadSingle();
                    block.WindCompensationTheta = reader.ReadSingle();
                    block.WindCompensationPhi = reader.ReadSingle();
                    block.StateX1 = reader.ReadSingle();
                    block.StateX2 = reader.ReadSingle();
                    block.StateX3 = reader.ReadSingle();
                    block.StateX4 = reader.ReadSingle();
                    block.StateX5 = reader.ReadSingle();
                    block.StateX6 = reader.ReadSingle();
                    block.MagnetoDebug1 = reader.ReadSingle();
                    block.MagnetoDebug2 = reader.ReadSingle();
                    block.MagnetoDebug3 = reader.ReadSingle();
                }
            }
            return block;
        }
    }
}
