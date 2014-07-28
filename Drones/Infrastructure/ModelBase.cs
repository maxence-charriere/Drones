using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Drones.Infrastructure
{
    public class ModelBase : INotifyPropertyChanged
    {
        // @Events
        public event PropertyChangedEventHandler PropertyChanged;


        // @Public
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
