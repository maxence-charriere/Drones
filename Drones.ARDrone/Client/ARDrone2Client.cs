using Drones.ARDrone.Client.NavData;
using Drones.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client
{
    public class ARDrone2Client : IDroneClient
    {
        // @Properties
        NavDataMode _navDataMode = NavDataMode.Demo;
        public NavDataMode NavDataMode
        {
            get
            {
                return _navDataMode;
            }
            set
            {
                _navDataMode = value;
            }
        }


        // @Public
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


        // @Private
    }
}
