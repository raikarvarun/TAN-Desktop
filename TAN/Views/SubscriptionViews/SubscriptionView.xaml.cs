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
using TAN.Notification.Utils;
using TAN.Notification;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    /// 


    public partial class SubscriptionView : UserControl
    {
        private static List<customerModel> _customers;
        private ObservableCollection<customerModel> _customersMaindata;
        private ObservableCollection<CustomerTransationmodel> _TranMaindata;
        private object _lockMutex = new object();
        private object _lockMutex1 = new object();
        private int currentProdcutID;
        public ObservableCollection<customerModel> CustomerMainData
        {
            get { return _customersMaindata; }
            set { _customersMaindata = value; }
        }
        public ObservableCollection<CustomerTransationmodel> TranMainData
        {
            get { return _TranMaindata; }
            set { _TranMaindata = value; }
        }
        public delegate void TEmpfun();
        private TEmpfun AddItemFun;
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public SubscriptionView(IEventAggregator events, IAPIHelper aPIHelper)
        {

            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _customersMaindata = new ObservableCollection<customerModel>();
            _TranMaindata = new ObservableCollection<CustomerTransationmodel>();
            BindingOperations.EnableCollectionSynchronization(CustomerMainData, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(TranMainData, _lockMutex1);
            ItemsTransation.ItemsSource = TranMainData;
            CustomerDataGrid.ItemsSource = CustomerMainData;
            assginParties();
            CustomerDataGrid.SelectedIndex = 0;

        }
        private void SaveButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(8, 1, null));
        }
        private void SubscriptionEditButtonClicked(object sender, RoutedEventArgs e)
        {
            customerModel custe = (customerModel)CustomerDataGrid.SelectedItem;
            OrderTableModel data = SubscriptionSqllite.getSingleOrderDataByCustomerIDandOrdertype(custe.customerID);
            _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(8, 2, data));
        }
        public void assginParties()
        {

            _ = LoadDataAsync();

        }

        private void PARTIES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            customerModel data = (customerModel)CustomerDataGrid.SelectedItem;
            if (data != null)
            {
                if (data.customerName != null)
                {
                    NameText.Text = data.customerName;
                    //SalePrice.Text = "₹ " + DisplayIndianCurrency(data.salePrice.ToString());
                    //StockQuantity.Text = data.productQuntity.ToString();
                    //PurchasePrice.Text = "₹ " + DisplayIndianCurrency(data.salePrice.ToString());
                    //StockValue.Text = "₹ " + DisplayIndianCurrency((data.productQuntity * data.productPrice).ToString());
                    assginTransaction(data.customerID);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            searchButtonDP.Visibility = Visibility.Collapsed;
            addPartyDP.Visibility = Visibility.Collapsed;
            searchTextDockPannel.Visibility = Visibility.Visible;
            searchTextBox.Focus();
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

                _customers = CustomerSqllite.readOnlySubcriptionCustomers();
                foreach (customerModel c in _customers)
                {
                    _customersMaindata.Add(c);
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
                _customersMaindata.Clear();
                foreach (customerModel c in _customers.Where(s => s.customerName.ToLower().Contains(key)).ToList())
                {
                    _customersMaindata.Add(c);
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
                _customersMaindata.Clear();
                foreach (customerModel c in _customers)
                {
                    _customersMaindata.Add(c);
                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            searchButtonDP.Visibility = Visibility.Visible;
            addPartyDP.Visibility = Visibility.Visible;
            searchTextDockPannel.Visibility = Visibility.Collapsed;
            searchTextBox.Clear();
            ReassginWholeData();
        }

        //Transation Logic
        //

        private List<CustomerTransationmodel> Transaction;
        private void assginTransaction(int customerId)
        {
            currentProdcutID = customerId;
            _TranMaindata.Clear();
            LoadTranDataAsync(customerId);
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

            _TranMaindata.Clear();
            foreach (CustomerTransationmodel c in Transaction.Where(s => s.Type.ToLower().Contains(key)).ToList())
            {
                _TranMaindata.Add(c);
            }

        }

        private void LoadTranDataAsync(int id)
        {


            Transaction = CustomerSqllite.readTransationbyId(id);
            foreach (CustomerTransationmodel c in Transaction)
            {
                _TranMaindata.Add(c);
            }

        }

        private async void deleteSubscription(object sender, RoutedEventArgs e)
        {
            CustomerTransationmodel data = (CustomerTransationmodel)ItemsTransation.SelectedItem;


            string msgtext = "Do you want to Delete Transaction?";
            string txt = "Mahesh Dairy Farm";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result1 = MessageBox.Show(msgtext, txt, button);
            switch (result1)
            {
                case MessageBoxResult.OK:
                    var admin = AdminTableSqlite.getAdminData();
                    var token = admin.adminToken;
                    var ans = await _apiHelper.deleteSubscription(token, data.orderID.ToString(), data.paymentID.ToString());
                    orderProductRelationModelSqlite.deleteByID(data.orderID.ToString());
                    OrderTableSqlite.deleteByID(data.orderID.ToString());
                    paymentSqlite.deleteByID(data.paymentID.ToString());
                    var notificationManager = new NotificationManager();
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Transaction Deleted Sucessfully",

                        Type = NotificationType.Success
                    });
                    assginTransaction(currentProdcutID);

                    break;
                case MessageBoxResult.Cancel:

                    break;
            }


        }
    }
}
