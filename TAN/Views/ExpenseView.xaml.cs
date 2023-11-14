using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ExpenseView.xaml
    /// </summary>
    public partial class ExpenseView : UserControl
    {

        private IEventAggregator _events;
        private IAPIHelper _aPIHelper;
        public ExpenseView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _aPIHelper = aPIHelper;

            ExpenseCat();
        }

        void ExpenseItem()
        {
            ExpenseMainItem.Content = new ExpenseItemView(_events, _aPIHelper);
            ItemsButton.Foreground = new SolidColorBrush(Colors.Black);
            ItemsButton.BorderThickness = new Thickness(0, 0, 0, 2);

            CategoryButton.BorderThickness = new Thickness(0);

            CategoryButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
        }
        
        private void ExpenseCat()
        {
            ExpenseMainItem.Content = new ExpenseCatView(_events, _aPIHelper);
            CategoryButton.Foreground = new SolidColorBrush(Colors.Black);
            CategoryButton.BorderThickness = new Thickness(0, 0, 0, 2);

            ItemsButton.BorderThickness = new Thickness(0);
            ItemsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
        }
        private void ItemsButton_Click(object sender, RoutedEventArgs e)
        {
            ExpenseItem();

        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ExpenseCat();
        }
    }
}
