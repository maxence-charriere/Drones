using Drones.Infrastructure;

namespace Drones.Client.Navigation
{
    public class Magneto : ModelBase
    {
        // @Properties
        Vector3 _raw = new Vector3();
        public Vector3 Raw
        {
            get
            {
                return _raw;
            }
            set
            {
                if (_raw != value)
                {
                    _raw = value;
                    RaisePropertyChanged();
                }
            }
        }

        Vector3 _rectified = new Vector3();
        public Vector3 Rectified
        {
            get
            {
                return _rectified;
            }
            set
            {
                if (_rectified != value)
                {
                    _rectified = value;
                    RaisePropertyChanged();
                }
            }
        }

        Vector3 _offset = new Vector3();
        public Vector3 Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                if (_offset != value)
                {
                    _offset = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
