using System.ComponentModel;

namespace Drones.Client.Navigation
{
    public enum DistanceMeasurementUnit
    {
        [Description("m")]
        Meters,

        [Description("km")]
        Kilometers,

        [Description("mi")]
        Miles,

        [Description("ft")]
        Feet
    }
}
