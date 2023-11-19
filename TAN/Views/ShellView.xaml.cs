using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window, IShellView
    {
        private AddPartiesView _addPartiesView { get; set; }
        private PaymentINorOutPageVIew _paymentInPageVIew { get; set; }
        private SalePageView _salePageView { get; set; }
        private AddSelectUnit _addSelectUnit { get; set; }
        private AddExpenseCat _addExpenseCat  { get; set; }
        private AddExpeneItemView _addExpeneItemView { get; set; }
        private ExpensePageView _expensePageView { get; set; }
        private SettingsView _settingsView { get; set; }
        private AddUserView _addUserView { get; set; }












        public ShellView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;


        }
        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    if (this.WindowState == WindowState.Maximized)
                    {
                        this.WindowState = WindowState.Normal;
                        NormalWindowButton.Visibility = Visibility.Visible;
                        MaximizeWindowButton.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.WindowState = WindowState.Maximized;
                        NormalWindowButton.Visibility = Visibility.Collapsed;
                        MaximizeWindowButton.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }
        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            NormalWindowButton.Visibility = Visibility.Visible;
            MaximizeWindowButton.Visibility = Visibility.Collapsed;
            Application.Current.Windows[0].WindowState = WindowState.Normal;
        }
        private void NormalWindow(object sender, RoutedEventArgs e)
        {
            NormalWindowButton.Visibility = Visibility.Collapsed;
            MaximizeWindowButton.Visibility = Visibility.Visible;
            Application.Current.Windows[0].WindowState = WindowState.Maximized;
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }
        public void addPartiesInShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _addPartiesView = new AddPartiesView(events, aPIHelper);
            ShellGridMain.Children.Add(_addPartiesView);
        }
        public void clearChildOfShellView()
        {
            ShellGridMain.Children.Remove(_addPartiesView);
        }

        public void addSalePagetoShellView(IEventAggregator events, IAPIHelper aPIHelper, int orderType)
        {
            _salePageView = new SalePageView(events, aPIHelper, orderType);
            ShellGridMain.Children.Add(_salePageView);
        }
        public void removeSalePagefromShellView()
        {
            ShellGridMain.Children.Remove(_salePageView);
        }
        


        public void addPaymentInShellView(IEventAggregator events, IAPIHelper aPIHelper ,int orderType)
        {
            _paymentInPageVIew = new PaymentINorOutPageVIew(events, aPIHelper, true,orderType);
            ShellGridMain.Children.Add(_paymentInPageVIew);
        }
        public void removePaymentChildOfShellView()
        {
            ShellGridMain.Children.Remove(_paymentInPageVIew);
        }

        public void addSelectUnitShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _addSelectUnit = new AddSelectUnit(events, aPIHelper);
            ShellGridMain.Children.Add(_addSelectUnit);
        }
        public void removeSelectUnitChildOfShellView()
        {
            ShellGridMain.Children.Remove(_addSelectUnit);
        }


        public void addExpenseCategoryShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _addExpenseCat = new AddExpenseCat(events, aPIHelper);
            ShellGridMain.Children.Add(_addExpenseCat);
        }
        public void removeExpenseCategoryChildOfShellView()
        {
            ShellGridMain.Children.Remove(_addExpenseCat);
        }

        public void addExpenseItemShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _addExpeneItemView = new AddExpeneItemView(events, aPIHelper);
            ShellGridMain.Children.Add(_addExpeneItemView);
        }
        public void removeExpenseItemChildOfShellView()
        {
            ShellGridMain.Children.Remove(_addExpeneItemView);
        }

        public void addExpensePageShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _expensePageView = new ExpensePageView(events, aPIHelper);
            ShellGridMain.Children.Add(_expensePageView);
        }
        public void removeExpensePageChildOfShellView()
        {
            ShellGridMain.Children.Remove(_expensePageView);
        }

        public void addSettingMainViewShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _settingsView = new SettingsView(events, aPIHelper);
            ShellGridMain.Children.Add(_settingsView);
        }
        public void removeSettingMainViewChildOfShellView()
        {
            ShellGridMain.Children.Remove(_settingsView);
        }

        public void addAddUserViewShellView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _addUserView = new AddUserView(events, aPIHelper);
            ShellGridMain.Children.Add(_addUserView);
        }
        public void removeAddUserViewChildOfShellView()
        {
            ShellGridMain.Children.Remove(_addUserView);
        }


    }
}
