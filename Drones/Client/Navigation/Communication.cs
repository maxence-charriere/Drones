using Drones.Infrastructure;

namespace Drones.Client.Navigation
{
    public class Communication : ModelBase
    {
        // @Properties
        CommunicationType _type = CommunicationType.Unknown;
        public CommunicationType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    RaisePropertyChanged();
                }
            }
        }

        double _linkQuality = 0;
        public double LinkQuality
        {
            get
            {
                return _linkQuality;
            }
            set
            {
                if (value <= 0)
                {
                    value = 0;
                }

                if (_linkQuality != value)
                {
                    _linkQuality = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
