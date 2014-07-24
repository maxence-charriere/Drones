namespace Drones.ARDrone.Client.Navdata
{
    public enum NavdataOptionTag : ushort
    {
        NavdataDemo,
        NavdataTime,
        NavdataRawMesures,
        NavdataPhysMeasures,
        NavdataGyrosOffsets,
        NavdataEulerAngles,
        NavdataReferences,
        NavdataTrims,
        NavdataRcReferences,
        NavdataPwm,
        NavdataAltitude,
        NavdataVisionRaw,
        NavdataVisionOf,
        NavdataVision,
        NavdataVisionPerf,
        NavdataTrackersSend,
        NavdataVisionDetect,
        NavdataWatchdog,
        NavdataAdcDataFrame,
        NavdataVideoStream,
        NavdataGames,
        NavdataPressureRaw,
        NavdataMagneto,
        NavdataWind,
        NavdataKalmanPressure,
        NavdataHdVideoStream,
        NavdataWifi,
        NavdataZimu3000,
        NavdataChecksum = 0xFFFF
    }
}
