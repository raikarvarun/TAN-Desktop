using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PartiesView.xaml
    /// </summary>
    public partial class PartiesView : UserControl, IPartiesView
    {
        private static List<customerModel> _customer;
        public PartiesView()
        {
            InitializeComponent();
            assginParties();

        }
        public void assginParties()
        {
            _customer = CustomerSqllite.readAll();
            PARTIES.ItemsSource = _customer;
            PARTIES.SelectedIndex = 0;
        }
        private void PARTIES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            customerModel custoemr = (customerModel)PARTIES.SelectedItem;
            if (custoemr != null)
            {
                if (custoemr.customerName != null || custoemr.customerMobile != null)
                {
                    NameText.Text = custoemr.customerName;
                    MobileText.Text = "Phone: " + custoemr.customerMobile;
                    assginpartiesTransation(custoemr.customerID);
                }
            }
        }

        private void assginpartiesTransation(int customerID)
        {
            List<CustomerTransationmodel> transationmodel = CustomerSqllite.readTransationbyId(customerID);
            PartiesTransation.ItemsSource = transationmodel;
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


    }
}
