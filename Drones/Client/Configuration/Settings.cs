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
                if (_verticalThrust != value)
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
                if (_horizontalThrust != value)
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
                if (_yawThrust != value)
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
                if (_maximumAltitude != value)
                {
                    _maximumAltitude = value;
                    RaisePropertyChanged();
                }
            }
        }

        HullType _hullType = HullType.Default;
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
