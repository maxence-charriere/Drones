using Drones.Infrastructure;
using System;

namespace Drones.Input
{
    public abstract class InputBase
    {
        // @Properties
        float _pitch = 0F;
        public float Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value;
            }
        }

        float _roll = 0F;
        public float Roll
        {
            get
            {
                return _roll;
            }
            set
            {
                _roll = value;
            }
        }

        float _gaz = 0F;
        public float Gaz
        {
            get
            {
                return _gaz;
            }
            set
            {
                _gaz = value;
            }
        }

        float _yaw = 0F;
        public float Yaw
        {
            get
            {
                return _yaw;
            }
            set
            {
                _yaw = value;
            }
        }

        public bool IsMotionless
        {
            get
            {
                return (Math.Abs(Pitch) < _precision
                    && Math.Abs(Roll) < _precision
                    && Math.Abs(Gaz) < _precision 
                    && Math.Abs(Yaw) < _precision);
            }
        }

        public abstract bool IsConnected { get; }


        // @Public
        public readonly string Name;

        public InputBase(string name)
        {
            Name = name;
        }

        public void Update(float pitch, float roll, float gaz, float yaw)
        {
            Pitch = pitch;
            Roll = roll;
            Gaz = gaz;
            Yaw = yaw;
        }


        // @Private
        readonly float _precision = 0.01F;
    }
}
