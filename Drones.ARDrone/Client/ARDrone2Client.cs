using Drones.ARDrone.Client.ATCommands;
using Drones.ARDrone.Client.Navdata;
using Drones.ARDrone.Client.NavData;
using Drones.Client;
using Drones.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client
{
    public class ARDrone2Client : WorkerBase, IDroneClient
    {
        // @Properties
        public readonly string Hostname;
        public readonly NavdataAcquisition NavdataAcquisition;
        public readonly ATCommandSender ATCommandSender;

        public bool IsActive
        {
            get
            {
                return IsAlive;
            }
        }

        public bool IsConnected
        {
            get
            {
                return NavdataAcquisition.IsAcquiring;
            }
        }


        // @Public
        public ARDrone2Client(string hostname = "192.168.1.1")
        {
            Hostname = hostname;
            NavdataAcquisition = new NavdataAcquisition(this);
            ATCommandSender = new ATCommandSender(this);
        }

        public void TakeOff()
        {
            throw new NotImplementedException();
        }

        public void Land()
        {
            throw new NotImplementedException();
        }

        public void Emergency()
        {
            throw new NotImplementedException();
        }

        public void EmergencyRecover()
        {
            throw new NotImplementedException();
        }

        public void Move(float roll, float pitch, float gaz, float yaw)
        {
            throw new NotImplementedException();
        }


        // @Protected
        protected override void Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!NavdataAcquisition.IsAlive)
                {
                    NavdataAcquisition.Start();
                }
                Thread.Sleep(10);
            }

            // Stop Navdata acquisition.
            if (NavdataAcquisition.IsAlive)
            {
                NavdataAcquisition.Stop();
            }

            // Stop sending AT commands.
            if (ATCommandSender.IsAlive)
            {
                ATCommandSender.Stop();
            }

        }

        protected override void DisposeOverride()
        {
            base.DisposeOverride();

            NavdataAcquisition.Dispose();
            ATCommandSender.Dispose();
        }


        // @Internal
        internal void OnNavdataAcquisitionStarted()
        {
            // Starting workers.
            if (!ATCommandSender.IsAlive)
            {
                ATCommandSender.Start();
            }
        }

        internal void OnNavdataAcquisitionStopped()
        {
            // Stopping workers.
            if (ATCommandSender.IsAlive)
            {
                ATCommandSender.Stop();
            }
        }

        internal void OnNavdataPacketAcquired(NavdataPacket packet)
        {
            // packet interpretation.
            // packet transformation.
            // Firing event acquired.
        }
    }
}
