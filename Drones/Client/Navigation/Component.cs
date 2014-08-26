using Drones.Infrastructure;

namespace Drones.Client.Navigation
{
    public class Component : ModelBase
    {
        // @Properties
        bool _isFunctional = true;
        public bool IsFunctional
        {
            get
            {
                return _isFunctional;
            }
            set
            {
                if (_isFunctional != value)
                {
                    _isFunctional = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _error;
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
