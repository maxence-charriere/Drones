using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Client
{
    public interface IDroneClient
    {
        // @Properties
        bool IsActive { get; }
        bool IsConnected { get; }

        // @Members
        Task<bool> ConnectAsync();
        void Disconnect();
        void TakeOff();
        void Land();
        void Emergency();
        void EmergencyRecover();
        void Move(float roll, float pitch, float gaz, float yaw);
    }
}
