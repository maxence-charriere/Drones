using Drones.Infrastructure;

namespace Drones.Client.Configuration
{
    public class Settings : ModelBase
    {
        // @Properties
        double _verticalThrust = 30;
        public double VerticalThrust
        {
            get
            {
                return _verticalThrust;
            }
            set
            {
                if (_verticalThrust != value && value >= 0 && value <= 100)
                {
                    _verticalThrust = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _horizontalThrust = 30;
        public double HorizontalThrust
        {
            get
            {
                return _horizontalThrust;
            }
            set
            {
                if (_horizontalThrust != value && value >= 0 && value <= 100)
                {
                    _horizontalThrust = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _yawThrust = 30;
        public double YawThrust
        {
            get
            {
                return _yawThrust;
            }
            set
            {
                if (_yawThrust != value && value >= 0 && value <= 100)
                {
                    _yawThrust = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _maximumAltitude = 3;
        public double MaximumAltitude
        {
            get
            {
                return _maximumAltitude;
            }
            set
            {
                if (_maximumAltitude != value && value > 0)
                {
                    _maximumAltitude = value;
                    RaisePropertyChanged();
                }
            }
        }

        HullType _hullType = HullType.Indoor;
        public HullType HullType
        {
            get
            {
                return _hullType;
            }
            set
            {
                if (_hullType != value)
                {
                    _hullType = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
