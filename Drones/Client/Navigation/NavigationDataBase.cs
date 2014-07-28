using Drones.Infrastructure;
using System;

namespace Drones.Client.Navigation
{
    public class NavigationDataBase : ModelBase, INavigationData
    {
        // @Properties
        double _pitch = 0;
        public double Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                if (_pitch != value)
                {
                    _pitch = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _roll = 0;
        public double Roll
        {
            get
            {
                return _roll;
            }
            set
            {
                if (_roll != value)
                {
                    _roll = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _yaw = 0;
        public double Yaw
        {
            get
            {
                return _yaw;
            }
            set
            {
                if (_yaw != value)
                {
                    _yaw = value;
                    RaisePropertyChanged();
                }
            }
        }

        Distance _altitude = new Distance();
        public Distance Altitude
        {
            get
            {
                return _altitude;
            }
            set
            {
                if (_altitude != value)
                {
                    _altitude = value;
                    RaisePropertyChanged();
                }
            }
        }

        DateTime _time = DateTime.Now;
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        Magneto _magneto = new Magneto();
        public Magneto Magneto
        {
            get
            {
                return _magneto;
            }
            set
            {
                if (_magneto != value)
                {
                    _magneto = value;
                    RaisePropertyChanged();
                }
            }
        }

        Battery _battery = new Battery();
        public Battery Battery
        {
            get
            {
                return _battery;
            }
            set
            {
                if (_battery != value)
                {
                    _battery = value;
                    RaisePropertyChanged();
                }
            }
        }

        Communication _communication = new Communication();
        public Communication Communication
        {
            get
            {
                return _communication;
            }
            set
            {
                if (_communication != value)
                {
                    _communication = value;
                    RaisePropertyChanged();
                }
            }
        }

        Video _video = new Video();
        public Video Video
        {
            get
            {
                return _video;
            }
            set
            {
                if (_video != value)
                {
                    _video = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
