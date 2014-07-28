using Drones.Infrastructure;
namespace Drones.Client.Navigation
{
    public class Speed : Vector3
    {
        SpeedMeasurementUnit _measurementUnit = SpeedMeasurementUnit.MetersPerSecond;
        public SpeedMeasurementUnit MeasurementUnit
        {
            get
            {
                return _measurementUnit;
            }
            set
            {
                if (_measurementUnit != value)
                {
                    _measurementUnit = value;
                    RaisePropertyChanged();
                }
            }
        }


        // @Public
        public Speed()
        {
        }

        public Speed(double x, double y, double z, SpeedMeasurementUnit measurementUnit = SpeedMeasurementUnit.MetersPerSecond)
        {
            X = x;
            Y = y;
            Z = z;
            MeasurementUnit = measurementUnit;
        }

        public void ChangeMeasurementUnit(SpeedMeasurementUnit measurementUnit)
        {
            switch (measurementUnit)
            {
                case SpeedMeasurementUnit.MetersPerSecond:
                    ToMetersPerSecond();
                    break;
                case SpeedMeasurementUnit.KilometersPerHour:
                    ToKilometersPerHour();
                    break;
                case SpeedMeasurementUnit.MilesPerHour:
                    ToMilesPerHour();
                    break;
                case SpeedMeasurementUnit.Knots:
                    ToKnots();
                    break;
                case SpeedMeasurementUnit.FeetPerSecond:
                    ToFeetPerSecond();
                    break;
                default:
                    break;
            }
        }


        // @Private
        void ToMetersPerSecond()
        {
            switch (MeasurementUnit)
            {
                case SpeedMeasurementUnit.KilometersPerHour:
                    X = X * 0.277777778;
                    Y = Y * 0.277777778;
                    Z = Z * 0.277777778;
                    break;
                case SpeedMeasurementUnit.MilesPerHour:
                    X = X * 0.44704;
                    Y = Y * 0.44704;
                    Z = Z * 0.44704;
                    break;
                case SpeedMeasurementUnit.Knots:
                    X = X * 0.514444444;
                    Y = Y * 0.514444444;
                    Z = Z * 0.514444444;
                    break;
                case SpeedMeasurementUnit.FeetPerSecond:
                    X = X * 0.3048;
                    Y = Y * 0.3048;
                    Z = Z * 0.3048;
                    break;
                default:
                    break;
            }
            MeasurementUnit = SpeedMeasurementUnit.MetersPerSecond;
        }

        void ToKilometersPerHour()
        {
            switch (MeasurementUnit)
            {
                case SpeedMeasurementUnit.MetersPerSecond:
                    X = X * 3.6;
                    Y = Y * 3.6;
                    Z = Z * 3.6;
                    break;
                case SpeedMeasurementUnit.MilesPerHour:
                    X = X * 1.609344;
                    Y = Y * 1.609344;
                    Z = Z * 1.609344;
                    break;
                case SpeedMeasurementUnit.Knots:
                    X = X * 1.852;
                    Y = Y * 1.852;
                    Z = Z * 1.852;
                    break;
                case SpeedMeasurementUnit.FeetPerSecond:
                    X = X * 1.09728;
                    Y = Y * 1.09728;
                    Z = Z * 1.09728;
                    break;
                default:
                    break;
            }
            MeasurementUnit = SpeedMeasurementUnit.KilometersPerHour;
        }

        void ToMilesPerHour()
        {
            switch (MeasurementUnit)
            {
                case SpeedMeasurementUnit.MetersPerSecond:
                    X = X * 2.23693629;
                    Y = Y * 2.23693629;
                    Z = Z * 2.23693629;
                    break;
                case SpeedMeasurementUnit.KilometersPerHour:
                    X = X * 0.621371192;
                    Y = Y * 0.621371192;
                    Z = Z * 0.621371192;
                    break;
                case SpeedMeasurementUnit.Knots:
                    X = X * 1.15077945;
                    Y = Y * 1.15077945;
                    Z = Z * 1.15077945;
                    break;
                case SpeedMeasurementUnit.FeetPerSecond:
                    X = X * 0.681818182;
                    Y = Y * 0.681818182;
                    Z = Z * 0.681818182;
                    break;
                default:
                    break;
            }
            MeasurementUnit = SpeedMeasurementUnit.MilesPerHour;
        }

        void ToKnots()
        {
            switch (MeasurementUnit)
            {
                case SpeedMeasurementUnit.MetersPerSecond:
                    X = X * 1.94384449;
                    Y = Y * 1.94384449;
                    Z = Z * 1.94384449;
                    break;
                case SpeedMeasurementUnit.KilometersPerHour:
                    X = X * 0.539956803;
                    Y = Y * 0.539956803;
                    Z = Z * 0.539956803;
                    break;
                case SpeedMeasurementUnit.MilesPerHour:
                    X = X * 0.868976242;
                    Y = Y * 0.868976242;
                    Z = Z * 0.868976242;
                    break;
                case SpeedMeasurementUnit.FeetPerSecond:
                    X = X * 0.592483801;
                    Y = Y * 0.592483801;
                    Z = Z * 0.592483801;
                    break;
                default:
                    break;
            }
            MeasurementUnit = SpeedMeasurementUnit.Knots;
        }

        void ToFeetPerSecond()
        {
            switch (MeasurementUnit)
            {
                case SpeedMeasurementUnit.MetersPerSecond:
                    X = X * 3.2808399;
                    Y = Y * 3.2808399;
                    Z = Z * 3.2808399;
                    break;
                case SpeedMeasurementUnit.KilometersPerHour:
                    X = X * 0.911344415;
                    Y = Y * 0.911344415;
                    Z = Z * 0.911344415;
                    break;
                case SpeedMeasurementUnit.MilesPerHour:
                    X = X * 1.46666667;
                    Y = Y * 1.46666667;
                    Z = Z * 1.46666667;
                    break;
                case SpeedMeasurementUnit.Knots:
                    X = X * 1.68780986;
                    Y = Y * 1.68780986;
                    Z = Z * 1.68780986;
                    break;
                default:
                    break;
            }
            MeasurementUnit = SpeedMeasurementUnit.FeetPerSecond;
        }
    }
}
