using Drones.ARDrone.Data.Navdata;
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

namespace Drones.ARDrone.Client.Navigation
{
    public class NavdataReceiver : WorkerBase
    {
        // @Event
        public event Action NavdataAcquisitionStarted;
        public event Action NavdataAcquisitionStoped;
        public event Action<NavdataPacket> NavdataPacketAcquired;


        // @Properties
        public bool IsAcquiring
        {
            get;
            private set;
        }


        // @Public
        public const int NavdataPort = 5554;
        public const int KeepAliveTimeout = 200;
        public const int NavdataTimeout = 2000;
        public readonly string Hostname;

        public NavdataReceiver(string hostname)
        {
            Hostname = hostname;
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine("NavdataReceiver started.");
            try
            {
                IsAcquiring = false;
                using (var udpClient = new UdpClient(NavdataPort))
                {
                    udpClient.Connect(Hostname, NavdataPort);
                    SendKeepAliveSignal(udpClient);

                    var ipEndpoint = new IPEndPoint(IPAddress.Any, NavdataPort);
                    Stopwatch swKeepAliveTimeout = Stopwatch.StartNew();
                    Stopwatch swNavdataTimeout = Stopwatch.StartNew();

                    while (!token.IsCancellationRequested && swNavdataTimeout.ElapsedMilliseconds < NavdataTimeout)
                    {
                        if (udpClient.Available > 0)
                        {
                            IsAcquiring = true;
                            RaiseNavdataAcquisitionStarted();

                            byte[] data = udpClient.Receive(ref ipEndpoint);
                            var packet = new NavdataPacket(data);
                            swNavdataTimeout.Restart();
                            RaiseNavdataPacketAcquired(packet);
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
                    RaiseNavdataAcquisitionStoped();
                }
                System.Diagnostics.Debug.WriteLine("NavdataReceiver stopped.");
            }
        }


        // @Private
        void SendKeepAliveSignal(UdpClient udpClient)
        {
            try
            {
                byte[] bytes = BitConverter.GetBytes(1);
                udpClient.Send(bytes, bytes.Length);
            }
            catch (Exception)
            {
            }
        }

        void RaiseNavdataAcquisitionStarted()
        {
            if (NavdataAcquisitionStarted != null)
            {
                NavdataAcquisitionStarted();
            }
        }

        void RaiseNavdataAcquisitionStoped()
        {
            if (NavdataAcquisitionStoped != null)
            {
                NavdataAcquisitionStoped();
            }
        }

        void RaiseNavdataPacketAcquired(NavdataPacket packet)
        {
            if (NavdataPacketAcquired != null)
            {
                NavdataPacketAcquired(packet);
            }
        }
    }
}
