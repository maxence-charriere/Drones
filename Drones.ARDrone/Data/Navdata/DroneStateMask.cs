using System;

namespace Drones.ARDrone.Data.Navdata
{
    [Flags]
    public enum DroneStateMask : uint
    {
        /// <summary> FLY MASK : (0) ardrone is landed, (1) ardrone is flying. </summary>
        FlyMask = 1U << 0,

        /// <summary> VIDEO MASK : (0) video disable, (1) video enable. </summary>
        VideoMask = 1U << 1,

        /// <summary> VISION MASK : (0) vision disable, (1) vision enable. </summary>
        VisionMask = 1U << 2,

        /// <summary> CONTROL ALGO : (0) euler angles control, (1) angular speed control. </summary>
        ControlMask = 1U << 3,

        /// <summary> ALTITUDE CONTROL ALGO : (0) altitude control inactive (1) altitude control active. </summary>
        AltitudeMask = 1U << 4,

        /// <summary> USER feedback : Start button state. </summary>
        UserFeedbackStarMaskt = 1U << 5,

        /// <summary> Control command ACK : (0) None, (1) one received. </summary>
        CommandMask = 1U << 6,

        /// <summary> CAMERA MASK : (0) camera not ready, (1) Camera ready. </summary>
        CameraMask = 1U << 7,

        /// <summary> Travelling mask : (0) disable, (1) enable. </summary>
        TravellingMask = 1U << 8,

        /// <summary> USB key : (0) usb key not ready, (1) usb key ready. </summary>
        UsbMask = 1U << 9,

        /// <summary> Navdata demo : (0) All navdata, (1) only navdata demo. </summary>
        NavdataDemoMask = 1U << 10,

        /// <summary> Navdata bootstrap : (0) options sent in all or demo mode, (1) no navdata options sent. </summary>
        NavdataBootstrap = 1U << 11,

        /// <summary> Motors status : (0) Ok, (1) Motors problem. </summary>
        MotorsMask = 1U << 12,

        /// <summary> Communication Lost : (1) com problem, (0) Com is ok. </summary>
        ComLostMask = 1U << 13,

        /// <summary> Software fault detected - user should land as quick as possible (1). </summary>
        SoftwareFault = 1U << 14,

        /// <summary> VBat low : (1) too low, (0) Ok. </summary>
        VBatLow = 1U << 15,

        /// <summary> User Emergency Landing : (1) User EL is ON, (0) User EL is OFF. </summary>
        UserEL = 1U << 16,

        /// <summary> Timer elapsed : (1) elapsed, (0) not elapsed. </summary>
        TimerElapsed = 1U << 17,

        /// <summary> Magnetometer calibration state : (0) Ok, no calibration needed, (1) not ok, calibration needed. </summary>
        MagnetoNeedCalib = 1U << 18,

        /// <summary> Angles : (0) Ok, (1) out of range. </summary>
        AnglesOutOfRange = 1U << 19,

        /// <summary> WIND MASK: (0) ok, (1) Too much wind. </summary>
        WindMask = 1U << 20,

        /// <summary> Ultrasonic sensor : (0) Ok, (1) deaf. </summary>
        UltrasoundMask = 1U << 21,

        /// <summary> Cutout system detection : (0) Not detected, (1) detected. </summary>
        CutoutMask = 1U << 22,

        /// <summary> PIC Version number OK : (0) a bad version number, (1) version number is OK. </summary>
        PicVersionMask = 1U << 23,

        /// <summary> ATCodec thread ON : (0) thread OFF (1) thread ON. </summary>
        ATCodecThreadOn = 1U << 24,

        /// <summary> Navdata thread ON : (0) thread OFF (1) thread ON. </summary>
        NavdataThreadOn = 1U << 25,

        /// <summary> Video thread ON : (0) thread OFF (1) thread ON. </summary>
        VideoThreadOn = 1U << 26,

        /// <summary> Acquisition thread ON : (0) thread OFF (1) thread ON. </summary>
        AcqThreadOn = 1U << 27,

        /// <summary> CTRL watchdog : (1) delay in control execution (> 5ms), (0) control is well scheduled. </summary>
        CtrlWatchdogMask = 1U << 28,

        /// <summary> ADC Watchdog : (1) delay in uart2 dsr (> 5ms), (0) uart2 is good. </summary>
        AdcWatchdogMask = 1U << 29,

        /// <summary> Communication Watchdog : (1) com problem, (0) Com is ok. </summary>
        ComWatchdogMask = 1U << 30,

        /// <summary> Emergency landing : (0) no emergency, (1) emergency. </summary>
        EmergencyMask = 1U << 31            
    }
}
