
using Caliburn.Micro;
using DataBaseManger.Model;
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
        public delegate void TEmpfun(int whichMode, productVersionModel data);
        public delegate void TEmpfun1();

        private TEmpfun additem1;
        private TEmpfun1 AddUnitsFun;

        private IEventAggregator _events;
        public ItemMainVIew(int menuTab, IEventAggregator events, TEmpfun tempfun, TEmpfun1 tempfun1)
        {
            InitializeComponent();
            _events = events;
            additem1 = tempfun;
            AddUnitsFun = tempfun1;


            if (menuTab == 0)
            {
                MainItem.Content = new ItemView(events, changeCurrentViewtoAddItems);

                ProductsButton.Foreground = new SolidColorBrush(Colors.Black);
                ProductsButton.BorderThickness = new Thickness(0, 0, 0, 2);

                UnitsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
                UnitsButton.BorderThickness = new Thickness(0);

            }
            else if (menuTab == 1)
            {
                MainItem.Content = new UnitView(_events, changeCurrentViewtoAddUnits);
                UnitsButton.Foreground = new SolidColorBrush(Colors.Black);
                UnitsButton.BorderThickness = new Thickness(0, 0, 0, 2);

                ProductsButton.BorderThickness = new Thickness(0);
                ProductsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
            }


        }
        public void changeCurrentViewtoAddItems(int wholeNo, productVersionModel product)
        {
            additem1(wholeNo, product);
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



            ProductsButton.Foreground = new SolidColorBrush(Colors.Black);
            ProductsButton.BorderThickness = new Thickness(0, 0, 0, 2);

            UnitsButton.BorderThickness = new Thickness(0);
            UnitsButton.Foreground = new SolidColorBrush(Color.FromRgb(171, 171, 171));
        }
    }
}
