using Drones.ARDrone.Data.Navdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Data.Configuration
{
    [Flags]
    public enum NavdataOptionFlag
    {
        None = 0,
        Demo = 1 << NavdataOptionTag.Demo,
        Time = 1 << NavdataOptionTag.Time,
        RawMeasures = 1 << NavdataOptionTag.RawMesures,
        PhysMeasures = 1 << NavdataOptionTag.PhysMeasures,
        GyrosOffsets = 1 << NavdataOptionTag.GyrosOffsets,
        EulerAngles = 1 << NavdataOptionTag.EulerAngles,
        References = 1 << NavdataOptionTag.References,
        Trims = 1 << NavdataOptionTag.Trims,
        RcReferences = 1 << NavdataOptionTag.RcReferences,
        Pwm = 1 << NavdataOptionTag.Pwm,
        Altitude = 1 << NavdataOptionTag.Altitude,
        VisionRaw = 1 << NavdataOptionTag.VisionRaw,
        VisionOf = 1 << NavdataOptionTag.VisionOf,
        Vision = 1 << NavdataOptionTag.Vision,
        VisionPerf = 1 << NavdataOptionTag.VisionPerf,
        TrackersSend = 1 << NavdataOptionTag.TrackersSend,
        VisionDetect = 1 << NavdataOptionTag.VisionDetect,
        Watchdog = 1 << NavdataOptionTag.Watchdog,
        AdcDataFrame = 1 << NavdataOptionTag.AdcDataFrame,
        VideoStream = 1 << NavdataOptionTag.VideoStream,
        Games = 1 << NavdataOptionTag.Games,
        PressureRaw = 1 << NavdataOptionTag.PressureRaw,
        Magneto = 1 << NavdataOptionTag.Magneto,
        Wind = 1 << NavdataOptionTag.Wind,
        KalmanPressure = 1 << NavdataOptionTag.KalmanPressure,
        HDVideoStream = 1 << NavdataOptionTag.HdVideoStream,
        WiFi = 1 << NavdataOptionTag.Wifi,
        Zimmu3000 = 1 << NavdataOptionTag.Zimmu3000,
        Nums = 1 << NavdataOptionTag.Nums,
        All = (1 << (NavdataOptionTag.Nums + 1)) - 1
    }
}
