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

        DroneStatus _status = DroneStatus.Landed;
        public DroneStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
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

        Component _frontLeftEngine = new Component();
        public Component FrontLeftEngine
        {
            get
            {
                return _frontLeftEngine;
            }
            set
            {
                if (_frontLeftEngine != value)
                {
                    _frontLeftEngine = value;
                    RaisePropertyChanged();
                }
            }
        }

        Component _frontRightEngine = new Component();
        public Component FrontRightEngine
        {
            get
            {
                return _frontRightEngine;
            }
            set
            {
                if (_frontRightEngine != value)
                {
                    _frontRightEngine = value;
                    RaisePropertyChanged();
                }
            }
        }

        Component _rearLeftEngine = new Component();
        public Component RearLeftEngine
        {
            get
            {
                return _rearLeftEngine;
            }
            set
            {
                if (_rearLeftEngine != value)
                {
                    _rearLeftEngine = value;
                    RaisePropertyChanged();
                }
            }
        }

        Component _rearRightEngine = new Component();
        public Component RearRightEngine
        {
            get
            {
                return _rearRightEngine;
            }
            set
            {
                if (_rearRightEngine != value)
                {
                    _rearRightEngine = value;
                    RaisePropertyChanged();
                }
            }
        }

        Component _sensors = new Component();
        public Component Sensors
        {
            get
            {
                return _sensors;
            }
            set
            {
                if (_sensors != value)
                {
                    _sensors = value;
                    RaisePropertyChanged();
                }
            }
        }

        Wind _wind = new Wind();
        public Wind Wind
        {
            get
            {
                return _wind;
            }
            set
            {
                if (_wind != value)
                {
                    _wind = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
