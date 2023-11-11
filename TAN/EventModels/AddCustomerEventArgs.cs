using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class AddCustomerEventArgs : RoutedEventArgs
    {
        
        public AddCustomerEventArgs()
        {
        }

        public AddCustomerEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public AddCustomerEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
