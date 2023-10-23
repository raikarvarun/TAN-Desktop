using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class ProductDataEventArgs : RoutedEventArgs
    {
        public productVersionModel SelectedProductData { get; set; }


        public ProductDataEventArgs()
        {
        }

        public ProductDataEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public ProductDataEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
