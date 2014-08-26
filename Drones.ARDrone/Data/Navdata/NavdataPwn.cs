using System;
using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataPwn : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public byte Motor1 { get; private set; }
        public byte Motor2 { get; private set; }
        public byte Motor3 { get; private set; }
        public byte Motor4 { get; private set; }
        public byte SatMotor1 { get; private set; }
        public byte SatMotor2 { get; private set; }
        public byte SatMotor3 { get; private set; }
        public byte SatMotor4 { get; private set; }
        public float GazFeedForward { get; private set; }
        public float GazAltitude { get; private set; }
        public float AltitudeIntegral { get; private set; }
        public float VzRef { get; private set; }
        public int UPitch { get; private set; }
        public int URoll { get; private set; }
        public int UYaw { get; private set; }
        public float YawUI { get; private set; }
        public int UPitchPlanif { get; private set; }
        public int URollPlanif { get; private set; }
        public int UYawPlanif { get; private set; }
        public float UGazPlanif { get; private set; }
        public ushort CurrentMotor1 { get; private set; }
        public ushort CurrentMotor2 { get; private set; }
        public ushort CurrentMotor3 { get; private set; }
        public ushort CurrentMotor4 { get; private set; }
        public float AltitudeProp { get; private set; }
        public float AltitudeDer { get; private set; }


        // @Public
        public NavdataPwn()
        {
        }

        public static NavdataPwn FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataPwn();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.Motor1 = reader.ReadByte();
                    block.Motor2 = reader.ReadByte();
                    block.Motor3 = reader.ReadByte();
                    block.Motor4 = reader.ReadByte();
                    block.SatMotor1 = reader.ReadByte();
                    block.SatMotor2 = reader.ReadByte();
                    block.SatMotor3 = reader.ReadByte();
                    block.SatMotor4 = reader.ReadByte();
                    block.GazFeedForward = reader.ReadSingle();
                    block.GazAltitude = reader.ReadSingle();
                    block.AltitudeIntegral = reader.ReadSingle();
                    block.VzRef = reader.ReadSingle();
                    block.UPitch = reader.ReadInt32();
                    block.URoll = reader.ReadInt32();
                    block.UYaw = reader.ReadInt32();
                    block.YawUI = reader.ReadSingle();
                    block.UPitchPlanif = reader.ReadInt32();
                    block.URollPlanif = reader.ReadInt32();
                    block.UYawPlanif = reader.ReadInt32();
                    block.UGazPlanif = reader.ReadSingle();
                    block.CurrentMotor1 = reader.ReadUInt16();
                    block.CurrentMotor2 = reader.ReadUInt16();
                    block.CurrentMotor3 = reader.ReadUInt16();
                    block.CurrentMotor4 = reader.ReadUInt16();
                    block.AltitudeProp = reader.ReadSingle();
                    block.AltitudeDer = reader.ReadSingle();
                }
            }
            return block;
        }

        public override string ToString()
        {
            return string.Format("Pwn -> Motor1({0}, {1}, {2}) | Motor1({3}, {4}, {5}) | Motor1({6}, {7}, {8}) | Motor1({9}, {10}, {11})",
                Motor1, SatMotor1, CurrentMotor1,
                Motor1, SatMotor2, CurrentMotor2,
                Motor1, SatMotor3, CurrentMotor3,
                Motor1, SatMotor4, CurrentMotor4);
        }
    }
}
