using Caliburn.Micro;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TAN.Core
{
    public class ObservableObject : Screen, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
