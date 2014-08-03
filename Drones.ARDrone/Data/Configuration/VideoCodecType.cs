﻿namespace Drones.ARDrone.Data.Configuration
{
    public enum VideoCodecType
    {
        NULL = 0,

        /// <summary> Value is used for START_CODE. </summary>
        UVLC = 0x20, 

        /// <summary> Not used. </summary>
        MJPEG,

        /// <summary> Not used. </summary>
        P263, 
        P264 = 0x40,
        MP4_360P = 0x80,
        H264_360P = 0x81,
        MP4_360P_H264_720P = 0x82,
        H264_720P = 0x83,
        MP4_360P_SLRS = 0x84,
        H264_360P_SLRS = 0x85,
        H264_720P_SLRS = 0x86,

        /// <summary> Resolution is automatically adjusted according to bitrate. </summary>
        H264_AUTO_RESIZE = 0x87, 
        MP4_360P_H264_360P = 0x88,
    }
}
    