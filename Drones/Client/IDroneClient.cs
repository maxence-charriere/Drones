using Drones.Client.Configuration;
using Drones.Client.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Client
{
    public interface IDroneClient : IDisposable
    {
        // @Events
        event Action<INavigationData> NavigationDataAcquired;

        // @Properties
        bool IsActive { get; }
        bool IsConnected { get; }
        Settings Settings { get; set;}


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
