using Drones.ARDrone.Data.Navdata;
using System;
using System.Diagnostics;

namespace Drones.ARDrone.Data.Navdata
{
    public class NavdataPacket
    {
        // @Properties
        bool _isValid = false;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            private set
            {
                _isValid = value;
            }
        }

        public NavdataHeader Header { get; private set; }
        public NavdataChecksum Checksum { get; private set; }
        public NavdataDemo Demo { get; private set; }
        public NavdataRawMesures RawMesures { get; private set; }
        public NavdataMagneto Magneto { get; private set; }
        public NavdataVideoStream VideoStream { get; private set; }
        public NavdataWifi Wifi { get; private set; }


        // @Public
        public readonly long Timestamp;
        public readonly byte[] Data;

        public NavdataPacket(byte[] data)
        {
            Timestamp = DateTime.UtcNow.Ticks;
            Data = data;
            IsValid = ParseData();
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
                    position += Header.Size;
                    while (position < Data.Length)
                    {
                        var option = NavdataOption.FromByteArray(Data, position);
                        switch ((NavdataOptionTag)option.Tag)
                        {
                            case NavdataOptionTag.Demo:
                                Demo = NavdataDemo.FromByteArray(Data, position);
                                position += Demo.Size;
                                break;
                            case NavdataOptionTag.RawMesures:
                                RawMesures = NavdataRawMesures.FromByteArray(Data, position);
                                position += RawMesures.Size;
                                break;
                            case NavdataOptionTag.Checksum:
                                Checksum = NavdataChecksum.FromByteArray(Data, position);
                                position += Checksum.Size;
                                break;
                            case NavdataOptionTag.Magneto:
                                Magneto = NavdataMagneto.FromByteArray(Data, position);
                                position += Magneto.Size;
                                break;
                            case NavdataOptionTag.VideoStream:
                                VideoStream = NavdataVideoStream.FromByteArray(Data, position);
                                position += VideoStream.Size;
                                break;
                            case NavdataOptionTag.Wifi:
                                Wifi = NavdataWifi.FromByteArray(Data, position);
                                position += Wifi.Size;
                                break;
                            default:
                                position += option.Size;
                                break;
                        }
                    }
                    return IsDataComplete();
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine("NavdataPacket.ParseData() : " + e.Message);
                return false;
            }
        }

        bool IsDataComplete()
        {
            return (Header != null && IsChecksumValid());
        }

        bool IsChecksumValid()
        {
            if (Checksum != null)
            {
                uint checksum = 0;
                for (int i = 0; i < Data.Length - Checksum.Size; ++i)
                {
                    checksum += Data[i];
                }
                return checksum == Checksum.Checksum;
            }
            return false;
        }
    }
}
