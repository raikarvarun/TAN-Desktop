using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class AddExpenseCatEventArgs : RoutedEventArgs
    {
        
        public AddExpenseCatEventArgs()
        {
        }

        public AddExpenseCatEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public AddExpenseCatEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
