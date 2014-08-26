using Drones.Infrastructure;
using System;

namespace Drones.Client.Navigation
{
    public class Wind : ModelBase
    {
        // Proeprties
        Speed _speed = new Speed();
        public Speed Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _angle = 0;
        public double Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                if (_angle != value)
                {
                    _angle = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
