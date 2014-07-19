using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Client
{
    public interface IDroneClient
    {
        // @Members
        void TakeOff();
        void Land();
        void Emergency();
        void EmergencyRecover();
        void Move(float roll, float pitch, float gaz, float yaw);
    }
}
