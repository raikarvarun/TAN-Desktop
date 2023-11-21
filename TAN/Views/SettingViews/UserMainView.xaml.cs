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

namespace TAN.Views.SettingViews
{
    /// <summary>
    /// Interaction logic for UserMainView.xaml
    /// </summary>
    public partial class UserMainView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public UserMainView(IEventAggregator events, IAPIHelper aPIHelper )
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;


            _usersMaindata = new ObservableCollection<adminModel>();
            
            BindingOperations.EnableCollectionSynchronization(UsersMainData, _lockMutex);
           
            
            PRODUCTS.ItemsSource = UsersMainData;
            assginParties();
            PRODUCTS.SelectedIndex = 0;
        }

        

        private static List<adminModel> _users;
        private ObservableCollection<adminModel> _usersMaindata;
        
        private object _lockMutex = new object();
        
        private int currentProdcutID;
        public ObservableCollection<adminModel> UsersMainData
        {
            get { return _usersMaindata; }
            set { _usersMaindata = value; }
        }
        
        
        
        private void SaveButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            _ = _events.PublishOnUIThreadAsync(new AddUserViewEventModel());
        }

        public void assginParties()
        {

            _ = LoadDataAsync();

        }

        private void PARTIES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adminModel product = (adminModel)PRODUCTS.SelectedItem;
            if (product != null)
            {
                //if (product.productName != null)
                //{
                //    NameText.Text = product.productName;
                //    SalePrice.Text = "₹ " + DisplayIndianCurrency(product.salePrice.ToString());
                //    StockQuantity.Text = product.productQuntity.ToString();
                //    PurchasePrice.Text = "₹ " + DisplayIndianCurrency(product.salePrice.ToString());
                //    StockValue.Text = "₹ " + DisplayIndianCurrency((product.productQuntity * product.productPrice).ToString());
                    
                //}
            }

        }
        private string DisplayIndianCurrency(string data)
        {
            decimal parsed = decimal.Parse(data, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            hindi.NumberFormat.CurrencySymbol = "";
            string text = string.Format(hindi, "{0:c}", parsed);
            return text;
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

                _users = AdminTableSqlite.readAll();
                foreach (adminModel c in _users)
                {
                    _usersMaindata.Add(c);
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
                _usersMaindata.Clear();
                foreach (adminModel c in _users.Where(s => s.adminEmail.ToLower().Contains(key)).ToList())
                {
                    _usersMaindata.Add(c);
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
                _usersMaindata.Clear();
                foreach (adminModel c in _users)
                {
                    _usersMaindata.Add(c);
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

        

        
    }
}
