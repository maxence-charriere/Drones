namespace Drones.ARDrone.Data.Navdata
{
    public enum NavdataOptionTag : ushort
    {
        Demo,
        Time,
        RawMesures,
        PhysMeasures,
        GyrosOffsets,
        EulerAngles,
        References,
        Trims,
        RcReferences,
        Pwm,
        Altitude,
        VisionRaw,
        VisionOf,
        Vision,
        VisionPerf,
        TrackersSend,
        VisionDetect,
        Watchdog,
        AdcDataFrame,
        VideoStream,
        Games,
        PressureRaw,
        Magneto,
        Wind,
        KalmanPressure,
        HdVideoStream,
        Wifi,
        Zimmu3000,
        Nums,
        Checksum = 0xFFFF
    }
}
