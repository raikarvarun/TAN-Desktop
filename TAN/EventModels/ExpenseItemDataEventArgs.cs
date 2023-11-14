using DataBaseManger.Model;
using System.Windows;

namespace TAN.EventModels
{
    public class ExpenseItemDataEventArgs : RoutedEventArgs
    {
        public ExpenseItemModel SelectedData { get; set; }


        public ExpenseItemDataEventArgs()
        {
        }

        public ExpenseItemDataEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public ExpenseItemDataEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
