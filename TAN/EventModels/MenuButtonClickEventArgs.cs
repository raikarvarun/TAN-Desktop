using System.Windows;
using System.Windows.Controls;

namespace TAN.EventModels
{
    public class MenuButtonClickEventArgs : RoutedEventArgs
    {
        public Button CurrentButton { get; set; }
        public MenuButtonClickEventArgs()
        {
        }

        public MenuButtonClickEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public MenuButtonClickEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
