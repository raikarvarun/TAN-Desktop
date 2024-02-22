using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for BankAccountsView.xaml
    /// </summary>
    public partial class BankAccountsView : UserControl
    {
        private static List<PaymentTypeModel> _paymenttype;
        public delegate void TEmpfun();

        private TEmpfun _tempfun;
        public BankAccountsView(TEmpfun tempfun)
        {
            InitializeComponent();
            assginParties();
            _tempfun = tempfun;
        }
        public void assginParties()
        {
            _paymenttype = PaymentTypeSqlite.readAll();
            PARTIES.ItemsSource = _paymenttype;
            PARTIES.SelectedIndex = 0;
        }
        private void PARTIES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PaymentTypeModel paymentType = (PaymentTypeModel)PARTIES.SelectedItem;
            if (paymentType != null)
            {
                if (paymentType.paymentTypeType != null || paymentType.paymentTypeName != null)
                {
                    NameText.Text = paymentType.paymentTypeType;
                    MobileText.Text = "Phone: " + paymentType.paymentTypeName;
                    assignBank(paymentType.paymentTypeId);
                }
            }
        }
        private void assignBank(int data)
        {
            List<BankTransactoin> bankTransactoin = PaymentTypeSqlite.getDatabyIDforBankTransaction(data);
            BankTransation.ItemsSource = bankTransactoin;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            searchButtonDP.Visibility = Visibility.Collapsed;
            addPartyDP.Visibility = Visibility.Collapsed;
            searchTextDockPannel.Visibility = Visibility.Visible;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<customerModel> customerModels = CustomerSqllite.searchCustomers(searchTextBox.Text);
            PARTIES.ItemsSource = customerModels;

        }

        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchButtonDP.Visibility = Visibility.Visible;
            addPartyDP.Visibility = Visibility.Visible;
            searchTextDockPannel.Visibility = Visibility.Collapsed;
            assginParties();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _tempfun();
        }
    }
}
