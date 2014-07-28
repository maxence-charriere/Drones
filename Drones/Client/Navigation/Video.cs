using Drones.Infrastructure;

namespace Drones.Client.Navigation
{
    public class Video : ModelBase
    {
        // @Properties
        uint _frameNumber = 0;
        public uint FrameNumber
        {
            get
            {
                return _frameNumber;
            }
            set
            {
                if (_frameNumber != value)
                {
                    _frameNumber = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _bitRate = 0;
        public double BitRate
        {
            get
            {
                return _bitRate;
            }
            set
            {
                if (_bitRate != value)
                {
                    _bitRate = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
