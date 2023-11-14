using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class AddProductrEventArgs : RoutedEventArgs
    {
        
        public AddProductrEventArgs()
        {
        }

        public AddProductrEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public AddProductrEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
