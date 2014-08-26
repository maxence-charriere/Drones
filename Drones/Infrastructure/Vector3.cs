namespace Drones.Infrastructure
{
    public class Vector3 : ModelBase
    {
        // @Properties
        double _x = 0;
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _y = 0;
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _z = 0;
        public double Z
        {
            get
            {
                return _z;
            }
            set
            {
                if (_z != value)
                {
                    _z = value;
                    RaisePropertyChanged();
                }
            }
        }

        // @Public
        public override string ToString()
        {
            return string.Format("x = {0}, y = {1}, z = {2}", X, Y, Z);
        }
    }
}
