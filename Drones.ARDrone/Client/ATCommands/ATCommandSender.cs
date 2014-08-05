using Drones.ARDrone.Data.Configuration;
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
        // @Public
        public const int CommandPort = 5556;
        public readonly ConcurrentQueue<ATCommand> CommandQueue = new ConcurrentQueue<ATCommand>();
        public readonly string Hostname;

        public ATCommandSender(string hostname)
        {
            Hostname = hostname;
        }

        public void Send(ATCommand command)
        {
            CommandQueue.Enqueue(command);
        }

        public void Send(Config config)
        {
            KeyValuePair<string, string> item;
            while (config.Changes.TryDequeue(out item))
            {
                if (string.IsNullOrEmpty(config.Custom.SessionId) == false &&
                    string.IsNullOrEmpty(config.Custom.ProfileId) == false &&
                    string.IsNullOrEmpty(config.Custom.ApplicationId) == false)
                {
                    Send(new ConfigIdsCommand(config.Custom.SessionId, config.Custom.ProfileId, config.Custom.ApplicationId));
                }

                Send(new ConfigCommand(item.Key, item.Value));
            }
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine("ATCommandSender started.");
            using (var udpClient = new UdpClient(CommandPort))
            {
                // Connection.
                udpClient.Connect(Hostname, CommandPort);

                // Sending first message.
                byte[] firstMessage = BitConverter.GetBytes(_sequenceNumber);
                udpClient.Send(firstMessage, firstMessage.Length);

                CommandQueue.Enqueue(ComWdgCommand.Default);
                Stopwatch swKeepAliveTimeout = Stopwatch.StartNew();

                // Loop launch.
                while (!token.IsCancellationRequested)
                {
                    if (CommandQueue.Count > 0)
                    {
                        using (var udpPacket = new MemoryStream())
                        {
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
            System.Diagnostics.Debug.WriteLine("ATCommandSender stopped.");
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
