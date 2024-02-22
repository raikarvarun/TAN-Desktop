
using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;


namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ExpenseView.xaml
    /// </summary>
    public partial class ItemMainVIew : UserControl
    {
        public delegate void TEmpfun();
        public delegate void TEmpfun1();

        private TEmpfun AddItemFun;
        private TEmpfun1 AddUnitsFun;

        private IEventAggregator _events;
        public ItemMainVIew(int menuTab , IEventAggregator events, TEmpfun tempfun  , TEmpfun1 tempfun1)
        {
            InitializeComponent();
            _events = events;
            AddItemFun = tempfun;
            AddUnitsFun = tempfun1;


            if(menuTab == 0)
            {
                MainItem.Content = new ItemView(events, changeCurrentViewtoAddItems);

                ProductsButton.Foreground = new SolidColorBrush(Colors.Black);
                ProductsButton.BorderThickness = new Thickness(0, 0, 0, 2);

                UnitsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
                UnitsButton.BorderThickness = new Thickness(0);

            }
            else if(menuTab == 1)
            {
                MainItem.Content = new UnitView(_events, changeCurrentViewtoAddUnits);
                UnitsButton.Foreground = new SolidColorBrush(Colors.Black);
                UnitsButton.BorderThickness = new Thickness(0, 0, 0, 2);

                ProductsButton.BorderThickness = new Thickness(0);
                ProductsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
            }

            
        }

        public void changeCurrentViewtoAddItems()
        {
            AddItemFun();
        }
        public void changeCurrentViewtoAddUnits()
        {
            AddUnitsFun();
        }

        private void UnitsButton_Click(object sender, RoutedEventArgs e)
        {
            MainItem.Content = new UnitView(_events, changeCurrentViewtoAddUnits);
            UnitsButton.Foreground = new SolidColorBrush(Colors.Black);
            UnitsButton.BorderThickness = new Thickness(0, 0, 0, 2);

            ProductsButton.BorderThickness = new Thickness(0);

            ProductsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));

        }

        private void PoductsButton_Click(object sender, RoutedEventArgs e)
        {
            MainItem.Content = new ItemView(_events, changeCurrentViewtoAddItems);



            ProductsButton.Foreground  = new SolidColorBrush(Colors.Black);
            ProductsButton.BorderThickness = new Thickness(0, 0 , 0 , 2);

            UnitsButton.BorderThickness = new Thickness(0);
            UnitsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
        }
    }
}
