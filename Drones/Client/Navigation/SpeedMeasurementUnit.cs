using System.ComponentModel;
namespace Drones.Client.Navigation
{
    public enum SpeedMeasurementUnit
    {
        [Description("m/s")]
        MetersPerSecond,

        [Description("km/h")]
        KilometersPerHour,

        [Description("mph")]
        MilesPerHour,

        [Description("knot")]
        Knots,

        [Description("ft/s")]
        FeetPerSecond,
    }
}
