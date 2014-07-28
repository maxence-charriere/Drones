using Drones.Infrastructure;

namespace Drones.Client.Navigation
{
    public class Distance : ModelBase
    {
        // @Properties
        double _value = 0;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged();
                }
            }
        }

        DistanceMeasurementUnit _measurementUnit = DistanceMeasurementUnit.Meters;
        public DistanceMeasurementUnit MeasurementUnit
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
        public Distance(double value = 0, DistanceMeasurementUnit measurementUnit = DistanceMeasurementUnit.Meters)
        {
            Value = value;
            MeasurementUnit = measurementUnit;
        }

        public void ChangeMeasurementUnit(DistanceMeasurementUnit measurementUnit)
        {
            switch (measurementUnit)
            {
                case DistanceMeasurementUnit.Meters:
                    break;
                case DistanceMeasurementUnit.Kilometers:
                    break;
                case DistanceMeasurementUnit.Miles:
                    break;
                case DistanceMeasurementUnit.Feet:
                    break;
                default:
                    break;
            }
        }


        // @Private
        void ToMeters()
        {
            switch (MeasurementUnit)
            {
                case DistanceMeasurementUnit.Kilometers:
                    Value *= 1000;
                    break;
                case DistanceMeasurementUnit.Miles:
                    Value *= 1609.344;
                    break;
                case DistanceMeasurementUnit.Feet:
                    Value *= 0.3048;
                    break;
                default:
                    break;
            }
            MeasurementUnit = DistanceMeasurementUnit.Meters;
        }

        void ToKilometers()
        {
            switch (MeasurementUnit)
            {
                case DistanceMeasurementUnit.Meters:
                    Value *= 0.001;
                    break;
                case DistanceMeasurementUnit.Miles:
                    Value *= 1.609344;
                    break;
                case DistanceMeasurementUnit.Feet:
                    Value *= 0.0003048;
                    break;
                default:
                    break;
            }
            MeasurementUnit = DistanceMeasurementUnit.Kilometers;
        }

        void ToMiles()
        {
            switch (MeasurementUnit)
            {
                case DistanceMeasurementUnit.Meters:
                    Value *= 0.000621371192;
                    break;
                case DistanceMeasurementUnit.Kilometers:
                    Value *= 0.621371192;
                    break;
                case DistanceMeasurementUnit.Feet:
                    Value *= 0.000189393939;
                    break;
                default:
                    break;
            }
            MeasurementUnit = DistanceMeasurementUnit.Miles;
        }

        void ToFeet()
        {
            switch (MeasurementUnit)
            {
                case DistanceMeasurementUnit.Meters:
                    Value *= 3.2808399;
                    break;
                case DistanceMeasurementUnit.Kilometers:
                    Value *= 3280.8399;
                    break;
                case DistanceMeasurementUnit.Miles:
                    Value *= 5280;
                    break;
                default:
                    break;
            }
            MeasurementUnit = DistanceMeasurementUnit.Feet;
        }
    }
}
