using Drones.Client.Configuration;
using Drones.Client.Navigation;
using Drones.Client.Video;
using Drones.Input;
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
        event Action<VideoFrame> VideoFrameDecoded;

        // @Properties
        bool IsActive { get; }
        bool IsConnected { get; }
        INavigationData CurrentNavigationData { get; }
        Settings Settings { get; set; }
        XBox360Input XBox360Input { get; set; }


        // @Members
        Task<bool> ConnectAsync();
        void Disconnect();
        void TakeOff();
        void Land();
        void Emergency();
        void EmergencyRecover();
        void CalibrateMagneto();
        void SwitchVideoChannel();
        void Move(float roll, float pitch, float gaz, float yaw);
        Task FlipAsync(FlipType type);
    }
}
