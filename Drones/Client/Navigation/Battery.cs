using Drones.Infrastructure;

namespace Drones.Client.Navigation
{
    public class Battery : ModelBase
    {
        // @Properties
        bool _isLow = true;
        public bool IsLow
        {
            get
            {
                return _isLow;
            }
            set
            {
                if (_isLow != value)
                {
                    _isLow = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _percentage = 0;
        public double Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                if (_percentage != value)
                {
                    _percentage = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _voltage = 0;
        public double Voltage
        {
            get
            {
                return _voltage;
            }
            set
            {
                if (_voltage != value)
                {
                    _voltage = value;
                    RaisePropertyChanged();
                }
            }
        }
        
    }
}
