using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataRawMesures : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public ushort[] RawAccs { get; private set; }
        public short[] RawGyros { get; private set; }
        public short[] RawGyros110 { get; private set; }

        /// <summary> Battery voltage raw (mV). </summary>
        ///
        /// <value> The v bat raw. </value>
        public uint VBatRaw { get; private set; }

        public ushort UsDebutEcho { get; private set; }
        public ushort UsFinEcho { get; private set; }
        public ushort UsAssociationEcho { get; private set; }
        public ushort UsDistanceEcho { get; private set; }
        public ushort UsCourbeTemps { get; private set; }
        public ushort UsCourbeValeur { get; private set; }
        public ushort UsCourbeRef { get; private set; }
        public ushort FlagEchoIni { get; private set; }
        public ushort NbEcho { get; private set; }
        public uint SumEcho { get; private set; }
        public int AltTempRaw { get; private set; }
        public short Gradient;


        // @Public
        public NavdataRawMesures()
        {
        }

        public static NavdataRawMesures FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataRawMesures();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();

                    block.RawAccs = new ushort[(int)DefAcc.NbAccs];
                    for (int i = 0; i < (int)DefAcc.NbAccs; ++i)
                    {
                        block.RawAccs[i] = reader.ReadUInt16();
                    }

                    block.RawGyros = new short[(int)DefGyro.NbGyros];
                    for (int i = 0; i < (int)DefGyro.NbGyros; ++i)
                    {
                        block.RawGyros[i] = reader.ReadInt16();
                    }

                    block.RawGyros110 = new short[2];
                    block.RawGyros110[0] = reader.ReadInt16();
                    block.RawGyros110[1] = reader.ReadInt16();

                    block.VBatRaw = reader.ReadUInt32();
                    block.UsDebutEcho = reader.ReadUInt16();
                    block.UsFinEcho = reader.ReadUInt16();
                    block.UsAssociationEcho = reader.ReadUInt16();
                    block.UsDistanceEcho = reader.ReadUInt16();
                    block.UsCourbeTemps = reader.ReadUInt16();
                    block.UsCourbeValeur = reader.ReadUInt16();
                    block.UsCourbeRef = reader.ReadUInt16();
                    block.FlagEchoIni = reader.ReadUInt16();
                    block.NbEcho = reader.ReadUInt16();
                    block.AltTempRaw = reader.ReadInt32();
                    block.Gradient = reader.ReadInt16();
                }
            }
            return block;
        }
    }
}
