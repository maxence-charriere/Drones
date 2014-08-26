using Drones.Infrastructure;
using System;

namespace Drones.Client.Navigation
{
    public class Distance : ModelBase, IComparable<Distance>, IEquatable<Distance>
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

        public Distance ChangeMeasurementUnit(DistanceMeasurementUnit measurementUnit)
        {
            var distance = new Distance(Value, MeasurementUnit);

            switch (measurementUnit)
            {
                case DistanceMeasurementUnit.Meters:
                    distance.ToMeters();
                    break;
                case DistanceMeasurementUnit.Kilometers:
                    distance.ToKilometers();
                    break;
                case DistanceMeasurementUnit.Miles:
                    distance.ToMiles();
                    break;
                case DistanceMeasurementUnit.Feet:
                    distance.ToFeet();
                    break;
                default:
                    break;
            }
            return distance;
        }

        public bool Equals(Distance other)
        {
            return Value == other.ChangeMeasurementUnit(MeasurementUnit).Value;
        }

        public int CompareTo(Distance other)
        {
            return Convert.ToInt32(Value - other.ChangeMeasurementUnit(MeasurementUnit).Value);
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
