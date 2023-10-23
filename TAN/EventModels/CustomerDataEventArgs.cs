using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class CustomerDataEventArgs : RoutedEventArgs
    {
        public customerModel SelectedcustomerData { get; set; }
        public CustomerDataEventArgs()
        {
        }

        public CustomerDataEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public CustomerDataEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
