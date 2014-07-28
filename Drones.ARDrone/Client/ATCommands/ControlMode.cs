namespace Drones.ARDrone.Client.ATCommands
{
    public enum ControlMode
    {
        /// <summary> Doing nothing. </summary>
        NoControlMode = 0,

        /// <summary> Not used. </summary>
        ARDroneUpdateControlMode,

        /// <summary> Not used. </summary>
        PicUpdateControlMode,

        /// <summary> Not used. </summary>
        LogsGetControlMode,

        /// <summary>
        /// Send active configuration file to a client through the 'control' socket UDP 5559.
        /// </summary>
        CfgGetControlMode,

        /// <summary>  Reset command mask in navdata. </summary>
        AckControlMode, 

        /// <summary> Requests the list of custom configuration IDs. </summary>
        CustomCfgGetControlMode 
    }
}
