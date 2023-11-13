using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class ExpenseCatDataEventArgs : RoutedEventArgs
    {
        public ExpenseCategoryModel SelectedData { get; set; }
        public ExpenseCatDataEventArgs()
        {
        }

        public ExpenseCatDataEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public ExpenseCatDataEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
