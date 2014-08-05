using System;
using System.IO;
using System.Text;

namespace Drones.ARDrone.Data.Video
{
    public class ParrotVideoEncapsulation : INativeBlock
    {
        // @Properties
        public ushort Size
        {
            get
            {
                return BlockSize;
            }
        }

        /// <summary> "PaVE" - used to identify the start of frame. </summary>
        ///
        /// <value> The signature. </value>
        public string Signature { get; private set; }

        /// <summary> Version code. </summary>
        ///
        /// <value> The version. </value>
        public byte Version { get; private set; }

        /// <summary> Codec of the following frame. </summary>
        ///
        /// <value> The video codec. </value>
        public byte VideoCodec { get; private set; }

        /// <summary> Size of the VideoEncapsulation. Should me equal to Size. </summary>
        ///
        /// <value> The size of the VideoEncapsulation. </value>
        public ushort HeaderSize { get; private set; }

        /// <summary> Amount of data following this PaVE. </summary>
        ///
        /// <value> The size of the payload. </value>
        public uint PayloadSize { get; private set; }

        public ushort EncodedStreamWidth { get; private set; }
        public ushort EncodedStreamHeight { get; private set; }
        public ushort DisplayWidth { get; private set; }
        public ushort DisplayHeight { get; private set; }

        /// <summary> Frame position inside the current stream. </summary>
        ///
        /// <value> The frame number. </value>
        public uint FrameNumber { get; private set; }

        /// <summary> Gets or sets the timestamp (In milliseconds). </summary>
        ///
        /// <value> The timestamp. </value>
        public uint Timestamp { get; private set; }

        /// <summary>
        /// Number of UDP packets containing the current decodable payload - currently unused.
        /// </summary>
        ///
        /// <value> The total number of chunks. </value>
        public byte TotalChunks { get; private set; }

        /// <summary> Position of the packet - first chunk is #0 - currenty unused. </summary>
        ///
        /// <value> The chunk index. </value>
        public byte ChunkIndex { get; private set; }

        public ParrotVideoEncapsulationFrameTypes FrameType { get; private set; }

        /// <summary> Special commands like end-of-stream or advertised frames. </summary>
        ///
        /// <value> The control. </value>
        public byte Control { get; private set; }

        /// <summary>
        /// Byte position of the current payload in the encoded stream - lower 32-bit word.
        /// </summary>
        ///
        /// <value> The stream byte position lw. </value>
        public uint StreamBytePositionLw { get; private set; }

        /// <summary>
        /// Byte position of the current payload in the encoded stream - upper 32-bit word.
        /// </summary>
        ///
        /// <value> The stream byte position uw. </value>
        public uint StreamBytePositionUw { get; private set; }

        /// <summary> This ID indentifies packets that should be recorded together. </summary>
        ///
        /// <value> The identifier of the stream. </value>
        public ushort StreamId { get; private set; }

        /// <summary> Number of slices composing the current frame. </summary>
        ///
        /// <value> The total number of slices. </value>
        public byte TotalSlices { get; private set; }

        /// <summary> Position of the current slice in the frame. </summary>
        ///
        /// <value> The slice index. </value>
        public byte SliceIndex { get; private set; }

        /// <summary>
        /// H.264 only : size of SPS inside payload - no SPS present if value is zero.
        /// </summary>
        ///
        /// <value> The size of the header 1. </value>
        public byte Header1Size { get; private set; }

        /// <summary>
        /// H.264 only : size of PPS inside payload - no PPS present if value is zero.
        /// </summary>
        ///
        /// <value> The size of the header 2. </value>
        public byte Header2Size { get; private set; }

        /// <summary> Padding to align on 48 bytes. </summary>
        ///
        /// <value> The reserved 2. </value>
        public byte[] Reserved2 { get; private set; }

        /// <summary> Size of frames announced as advertised frame. </summary>
        ///
        /// <value> The size of the advertised. </value>
        public uint AdvertisedSize { get; private set; }

        /// <summary> Padding to align on 64 bytes. </summary>
        ///
        /// <value> The reserved 3. </value>
        public byte[] reserved3 { get; private set; }


        // @Public
        public const ushort BlockSize = (sizeof(byte) * 4)  // Signature.
            + (sizeof(byte) * 2)                            // Version to VideoCodec.
            + sizeof(ushort)                                // HeaderSize.
            + sizeof(uint)                                  // PayloadSize.
            + (sizeof(ushort) * 4)                          // EncodedStreamWidth to DisplayHeight.
            + (sizeof(uint) * 2)                            // FrameNumber to Timestamp.
            + (sizeof(byte) * 4)                            // TotalChunks to Control.
            + (sizeof(uint) * 2)                            // StreamBytePositionLw to StreamBytePositionUw.
            + sizeof(ushort)                                // StreamId.
            + (sizeof(byte) * 4)                            // TotalSlices to Header2Size.
            + (sizeof(byte) * 2)                            // Reserved2.
            + sizeof(uint)                                  // AdvertisedSize.
            + (sizeof(byte) * 12);                          // Reserved3.

        public static bool TryParse(out ParrotVideoEncapsulation videoEncapsulation, byte[] data, uint position)
        {
            var block = new ParrotVideoEncapsulation();
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    block.Signature = Encoding.UTF8.GetString(reader.ReadBytes(4));
                    if (block.Signature == "PaVE")
                    {
                        block.Version = reader.ReadByte();
                        block.VideoCodec = reader.ReadByte();
                        block.HeaderSize = reader.ReadUInt16();
                        block.PayloadSize = reader.ReadUInt32();
                        block.EncodedStreamWidth = reader.ReadUInt16();
                        block.EncodedStreamHeight = reader.ReadUInt16();
                        block.DisplayWidth = reader.ReadUInt16();
                        block.DisplayHeight = reader.ReadUInt16();
                        block.FrameNumber = reader.ReadUInt32();
                        block.Timestamp = reader.ReadUInt32();
                        block.TotalChunks = reader.ReadByte();
                        block.ChunkIndex = reader.ReadByte();
                        block.FrameType = (ParrotVideoEncapsulationFrameTypes)reader.ReadByte();
                        block.Control = reader.ReadByte();
                        block.StreamBytePositionLw = reader.ReadUInt32();
                        block.StreamBytePositionUw = reader.ReadUInt32();
                        block.StreamId = reader.ReadUInt16();
                        block.TotalSlices = reader.ReadByte();
                        block.SliceIndex = reader.ReadByte();
                        block.Header1Size = reader.ReadByte();
                        block.Header2Size = reader.ReadByte();
                        block.Reserved2 = reader.ReadBytes(2);
                        block.AdvertisedSize = reader.ReadUInt32();
                        block.reserved3 = reader.ReadBytes(12);

                        videoEncapsulation = block;
                        return true;
                    }
                    videoEncapsulation = null;
                    return false;
                }
            }
        }
    }
}
