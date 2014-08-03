namespace Drones.ARDrone.Data.Configuration
{
    public enum VideoBitrateControlMode
    {
        /// <summary> Bitrate set to video:max_bitrate. </summary>
        Disabled = 0,

        /// <summary> Video bitrate varies in [250;video:max_bitrate] kbps. </summary>
        Dynamic,

        /// <summary> Video stream bitrate is fixed by the video:bitrate key. </summary>
        Manual 
    }
}
