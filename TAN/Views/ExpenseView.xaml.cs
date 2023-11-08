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

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ExpenseView.xaml
    /// </summary>
    public partial class ExpenseView : UserControl
    {
        public ExpenseView()
        {
            InitializeComponent();
            CategoryButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
            ItemsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));

            ExpenseMainItem.Content = new ExpenseCatView();
            CategoryButton.Foreground = new SolidColorBrush(Colors.Black);
            CategoryButton.BorderThickness = new Thickness(0, 0, 0, 2);
        }

        

        private void ItemsButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsButton.Foreground = new SolidColorBrush(Colors.Black);
            ItemsButton.BorderThickness = new Thickness(0, 0, 0, 2);

            CategoryButton.BorderThickness = new Thickness(0);

            CategoryButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));

        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ExpenseMainItem.Content = new ExpenseCatView();
            CategoryButton.Foreground  = new SolidColorBrush(Colors.Black);
            CategoryButton.BorderThickness = new Thickness(0, 0 , 0 , 2);

            ItemsButton.BorderThickness = new Thickness(0);
            ItemsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
        }
    }
}
