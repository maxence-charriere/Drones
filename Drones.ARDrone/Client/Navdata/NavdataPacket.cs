using Drones.ARDrone.Client.Navdata.Blocks;
using System;
using System.Diagnostics;

namespace Drones.ARDrone.Client.Navdata
{
    public class NavdataPacket
    {
        // @Properties
        bool _isCorrupted = false;
        public bool IsCorrupted
        {
            get
            {
                return _isCorrupted;
            }
            private set
            {
                _isCorrupted = value;
            }
        }

        public NavdataHeader Header { get; private set; }
        public NavdataChecksum Checksum { get; private set; }


        // @Public
        public readonly long Timestamp;
        public readonly byte[] Data;

        public NavdataPacket(byte[] data)
        {
            Timestamp = DateTime.UtcNow.Ticks;
            Data = data;
            IsCorrupted = ParseData() == true;
        }


        // @Private
        bool ParseData()
        {
            try
            {
                uint position = 0;
                Header = NavdataHeader.FromByteArray(Data, position);
                if (Header.IsValid)
                {
                    position += NavdataHeader.Size;
                    while (position < Data.Length)
                    {
                        var option = NavdataOption.FromByteArray(Data, position);
                        switch ((NavdataOptionTag)option.Tag)
                        {
                            case NavdataOptionTag.NavdataDemo:
                                // implement navdata demo.
                                break;
                            case NavdataOptionTag.NavdataChecksum:
                                Checksum = NavdataChecksum.FromByteArray(Data, position);
                                position += Checksum.Size;
                                break;
                            default:
                                position += option.Size;
                                break;
                        }
                    }
                    // Verify Checksum;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine("NavdataPacket.ParseData() : " + e.Message);
                return false;
            }
        }

    }
}
