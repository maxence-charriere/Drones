using Drones.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.ATCommands
{
    public class ATCommandSender : WorkerBase
    {
        // @Properties
        public const int CommandPort = 5556;
        public const int KeepAliveTimeout = 20;
        public readonly ConcurrentQueue<ATCommand> CommandQueue = new ConcurrentQueue<ATCommand>();
        public readonly ARDrone2Client DroneClient;


        // @Public
        public ATCommandSender(ARDrone2Client droneClient)
        {
            DroneClient = droneClient;
        }

        public void Send(ATCommand command)
        {
            CommandQueue.Enqueue(command);
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            using (var udpClient = new UdpClient(CommandPort))
            {
                // Connection.
                udpClient.Connect(DroneClient.Hostname, CommandPort);

                // Sending first message.
                byte[] firstMessage = BitConverter.GetBytes(_sequenceNumber);
                udpClient.Send(firstMessage, firstMessage.Length);

                CommandQueue.Enqueue(ComWdgCommand.Default);
                Stopwatch swKeepAliveTimeout = Stopwatch.StartNew();

                // Loop launch.
                while (!token.IsCancellationRequested)
                {
                    bool isResetComWatchdogCmdRequired = swKeepAliveTimeout.ElapsedMilliseconds > KeepAliveTimeout;
                    if (CommandQueue.Count > 0 || isResetComWatchdogCmdRequired)
                    {
                        using (var udpPacket = new MemoryStream())
                        {
                            if (isResetComWatchdogCmdRequired)
                            {
                                FillUdpPacket(udpPacket, ComWdgCommand.Default);
                                swKeepAliveTimeout.Restart();
                            }

                            ATCommand command = null;
                            while (CommandQueue.TryDequeue(out command))
                            {
                                FillUdpPacket(udpPacket, command);
                            }
                            var packetAsArray = udpPacket.ToArray();
                            udpClient.Send(packetAsArray, packetAsArray.Length);
                        }
                    }
                    Thread.Sleep(5);
                }
            }
        }


        // @Private
        int _sequenceNumber = 1;

        void FillUdpPacket(Stream s, ATCommand command)
        {
            var bytes = command.ToByteArray(_sequenceNumber);
            s.Write(bytes, 0, bytes.Length);
            ++_sequenceNumber;
        }
    }
}
