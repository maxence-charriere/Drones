using Drones.ARDrone.Data.Math;
using System;
using System.IO;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataDemo : INativeBlock
    {
        // @Properties
        public ushort Tag { get; private set; }
        public ushort Size { get; private set; }
        public ControlState State { get; private set; }

        /// <summary> Battery voltage filtered (mV). </summary>
        ///
        /// <value> The battery voltage. </value>
        public uint VBatFlyingPercentage { get; private set; }

        /// <summary> UAV's pitch (in milli-degree). </summary>
        ///
        /// <value> The theta. </value>
        public float Theta { get; private set; }

        /// <summary> UAV's roll (in milli-degree). </summary>
        ///
        /// <value> The phi. </value>
        public float Phi { get; private set; }

        /// <summary> UAV's yaw (in milli-degree). </summary>
        ///
        /// <value> The state. </value>
        public float Psi { get; private set; }

        /// <summary> UAV's altitude in centimeters. </summary>
        ///
        /// <value> The state. </value>
        public int Altitude { get; private set; }

        /// <summary> UAV's estimated linear velocity on X axis. </summary>
        ///
        /// <value> The vx. </value>
        public float VX { get; private set; }

        /// <summary> UAV's estimated linear velocity on Y axis. </summary>
        ///
        /// <value> The vy. </value>
        public float VY { get; private set; }

        /// <summary> UAV's estimated linear velocity on ZD axis. </summary>
        ///
        /// <value> The vz. </value>
        public float VZ { get; private set; }

        /// <summary> Streamed frame index. Not used -> To integrate in video stage. </summary>
        ///
        /// <value> The total number of frames. </value>
        public uint NumFrames { get; private set; }

        /// <summary> Type of tag searched in detection. </summary>
        ///
        /// <value> The type of the detection camera. </value>
        public uint DetectionCameraType { get; private set; }


        // @Public
        public NavdataDemo()
        {
        }

        public static NavdataDemo FromByteArray(byte[] data, uint position)
        {
            var block = new NavdataDemo();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Tag = reader.ReadUInt16();
                    block.Size = reader.ReadUInt16();
                    block.State = (ControlState)(reader.ReadUInt32() >> 0x10);
                    block.VBatFlyingPercentage = reader.ReadUInt32();
                    block.Theta = reader.ReadSingle();
                    block.Phi = reader.ReadSingle();
                    block.Psi = reader.ReadSingle();
                    block.Altitude = reader.ReadInt32();
                    block.VX = reader.ReadSingle();
                    block.VY = reader.ReadSingle();
                    block.VZ = reader.ReadSingle();
                    block.NumFrames = reader.ReadUInt32();

                    // Deprecated space.
                    ms.Position += Matrix33.DataSize;
                    ms.Position += Vector31.DataSize;
                    ms.Position += sizeof(uint);

                    block.DetectionCameraType = reader.ReadUInt32();
                }
            }
            return block;
        }
    }
}
