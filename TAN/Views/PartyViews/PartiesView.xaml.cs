using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PartiesView.xaml
    /// </summary>
    public partial class PartiesView : UserControl
    {










        private static List<customerModel> _customer;
        private ObservableCollection<customerModel> _customerMaindata;
        private ObservableCollection<CustomerTransationmodel> _ItemTranMaindata;
        private object _lockMutex = new object();
        private object _lockMutex1 = new object();
        private int currentProdcutID;
        public ObservableCollection<customerModel> CustomerMainData
        {
            get { return _customerMaindata; }
            set { _customerMaindata = value; }
        }
        public ObservableCollection<CustomerTransationmodel> ItemTranMainData
        {
            get { return _ItemTranMaindata; }
            set { _ItemTranMaindata = value; }
        }
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public PartiesView(IEventAggregator events, IAPIHelper aPIHelper)
        {

            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _customerMaindata = new ObservableCollection<customerModel>();
            _ItemTranMaindata = new ObservableCollection<CustomerTransationmodel>();
            BindingOperations.EnableCollectionSynchronization(CustomerMainData, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(ItemTranMainData, _lockMutex1);
            PartiesTransation.ItemsSource = ItemTranMainData;
            PARTIES.ItemsSource = CustomerMainData;
            assginParties();
            PARTIES.SelectedIndex = 0;

        }
        private void AddPartyButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            _ = _events.PublishOnUIThreadAsync(new AddPartyEventModel(1, null));
        }
        private void EditCustomerButtonClick(object sender, RoutedEventArgs e)
        {
            customerModel custoemr = (customerModel)PARTIES.SelectedItem;
            _ = _events.PublishOnUIThreadAsync(new AddPartyEventModel(2, custoemr));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            searchButtonDP.Visibility = Visibility.Collapsed;
            addPartyDP.Visibility = Visibility.Collapsed;
            searchTextDockPannel.Visibility = Visibility.Visible;
        }
        public void assginParties()
        {

            _ = LoadDataAsync();

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
                    assginTransaction(custoemr.customerID);
                }
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {



            string searchKey = searchTextBox.Text.ToLower();
            if (searchKey == "")
            {
                ReassginWholeData();

            }
            else
            {

                SearchData(searchKey);
            }



        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            searchButtonDP.Visibility = Visibility.Visible;
            addPartyDP.Visibility = Visibility.Visible;
            searchTextDockPannel.Visibility = Visibility.Collapsed;
            searchTextBox.Clear();
            ReassginWholeData();
        }
        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                searchButtonDP.Visibility = Visibility.Visible;
                addPartyDP.Visibility = Visibility.Visible;
                searchTextDockPannel.Visibility = Visibility.Collapsed;
                searchTextBox.Clear();

                ReassginWholeData();
            }

        }


        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {

                _customer = CustomerSqllite.readAll();
                foreach (customerModel c in _customer)
                {
                    _customerMaindata.Add(c);
                }
            });
        }

        private void SearchData(string key)
        {
            _ = SearchDataAsync(key);
        }
        private Task SearchDataAsync(string key)
        {
            return Task.Factory.StartNew(() =>
            {
                _customerMaindata.Clear();
                foreach (customerModel c in _customer.Where(s => s.customerName.ToLower().Contains(key)).ToList())
                {
                    _customerMaindata.Add(c);
                }
            });
        }

        private void ReassginWholeData()
        {
            _ = ReassginWholeDataAsync();
        }
        private Task ReassginWholeDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                _customerMaindata.Clear();
                foreach (customerModel c in _customer)
                {
                    _customerMaindata.Add(c);
                }
            });
        }



        //Transation Logic
        //

        private List<CustomerTransationmodel> itemTransaction;
        private void assginTransaction(int productID)
        {
            currentProdcutID = productID;
            _ItemTranMaindata.Clear();
            LoadTranDataAsync(productID);
        }
        private void CloseSearchButton1_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox1.Clear();
            assginTransaction(currentProdcutID);
        }



        private void searchTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchKey = searchTextBox1.Text.ToLower();
            if (searchKey == "")
            {
                CloseSearchButton1.Visibility = Visibility.Collapsed;
                assginTransaction(currentProdcutID);
            }
            else
            {
                CloseSearchButton1.Visibility = Visibility.Visible;
                SearchData1(searchKey);
            }
        }

        private void searchTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox1.Text == "")
            {

                searchTextBox1.Clear();

                assginTransaction(currentProdcutID);
            }
        }
        private void SearchData1(string key)
        {
            SearchData1Async(key);
        }
        private void SearchData1Async(string key)
        {

            _ItemTranMaindata.Clear();
            foreach (CustomerTransationmodel c in itemTransaction.Where(s => s.Type.ToLower().Contains(key)).ToList())
            {
                _ItemTranMaindata.Add(c);
            }

        }

        private void LoadTranDataAsync(int customerID)
        {


            itemTransaction = CustomerSqllite.readTransationbyId(customerID); ;
            foreach (CustomerTransationmodel c in itemTransaction)
            {
                _ItemTranMaindata.Add(c);
            }

        }



    }
}
