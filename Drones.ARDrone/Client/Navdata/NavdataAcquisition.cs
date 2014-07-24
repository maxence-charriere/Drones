using Drones.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.Navdata
{
    public class NavdataAcquisition : WorkerBase
    {
        // @Properties
        public const int NavdataPort = 5554;
        public const int KeepAliveTimeout = 200;
        public const int NavdataTimeout = 2000;
        public readonly ARDrone2Client DroneClient;

        public bool IsAcquiring
        {
            get;
            private set;
        }


        // @Public
        public NavdataAcquisition(ARDrone2Client droneClient)
        {
            DroneClient = droneClient;
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            try
            {
                IsAcquiring = false;
                using (var udpClient = new UdpClient(NavdataPort))
                {
                    udpClient.Connect(DroneClient.Hostname, NavdataPort);
                    SendKeepAliveSignal(udpClient);

                    var ipEndpoint = new IPEndPoint(IPAddress.Any, NavdataPort);
                    Stopwatch swKeepAliveTimeout = Stopwatch.StartNew();
                    Stopwatch swNavdataTimeout = Stopwatch.StartNew();

                    while (!token.IsCancellationRequested && swNavdataTimeout.ElapsedMilliseconds < NavdataTimeout)
                    {
                        if (udpClient.Available > 0)
                        {
                            IsAcquiring = true;
                            DroneClient.OnNavdataAcquisitionStarted();

                            byte[] data = udpClient.Receive(ref ipEndpoint);
                            var packet = new NavdataPacket(data);
                            swNavdataTimeout.Restart();
                            DroneClient.OnNavdataPacketAcquired(packet);
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }

                        if (swKeepAliveTimeout.ElapsedMilliseconds > KeepAliveTimeout)
                        {
                            SendKeepAliveSignal(udpClient);
                            swKeepAliveTimeout.Restart();
                        }
                    }
                }
            }
            finally
            {
                if (IsAcquiring)
                {
                    IsAcquiring = false;
                    DroneClient.OnNavdataAcquisitionStopped();
                }
            }
        }


        // @Private
        void SendKeepAliveSignal(UdpClient udpClient)
        {
            byte[] bytes = BitConverter.GetBytes(1);
            udpClient.Send(bytes, bytes.Length);
        }
    }
}
