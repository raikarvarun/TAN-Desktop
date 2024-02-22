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
using TAN.ViewModels;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ReportMainView.xaml
    /// </summary>
    public partial class ReportMainView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public ReportMainView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            PrevButton = SaleButton;
            PrevButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 11, 143, 197));
            PrevButton.Background = new SolidColorBrush(Color.FromArgb(255, 230, 230, 234));
            
            PrevButton.FontWeight = FontWeights.Medium;

        }




        Button PrevButton;
        private void ChangeSelection(object sender, RoutedEventArgs e)
        {

            PrevButton.BorderBrush = Brushes.Transparent;
            PrevButton.Background = Brushes.Transparent;
            
            PrevButton.FontWeight = FontWeights.Regular;
            


            Button currButton = sender as Button;
            currButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 11, 143, 197));
            currButton.Background = new SolidColorBrush(Color.FromArgb(255, 230, 230, 234));
            
            currButton.FontWeight = FontWeights.Medium;


            PrevButton = currButton;


        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender , e );
            MainWindow.Content = new CommanNavigationViewModel(1, _events, _apiHelper);
        }

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
            MainWindow.Content = new CommanNavigationViewModel(2, _events, _apiHelper);
        }

        private void DaybookButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
        }

        private void AllTransactionsButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
        }

        private void ProfitAndLossButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
        }

        private void BillWiseProfitButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
        }

        private void PartyStatementButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void PartyWiseProfitAndLossButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void AllpartiesButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void PartyReportByItemButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void SalePurchaseByPartyButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void StockSummeryButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void ItemWiseProfitAndLossButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void StockDetailButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void BankStatementButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }
    }
}
