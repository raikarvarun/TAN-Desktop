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
    /// Interaction logic for ItemView.xaml
    /// </summary>
    /// 


    public partial class ExpenseCatView : UserControl
    {
        private static List<productVersionModel> _products;
        private ObservableCollection<productVersionModel> _productsMaindata;
        private ObservableCollection<ItemTransactionModel> _ItemTranMaindata;
        private object _lockMutex = new object();
        private object _lockMutex1 = new object();
        private int currentProdcutID;
        public ObservableCollection<productVersionModel> ProductMainData
        {
            get { return _productsMaindata; }
            set { _productsMaindata = value; }
        }
        public ObservableCollection<ItemTransactionModel> ItemTranMainData
        {
            get { return _ItemTranMaindata; }
            set { _ItemTranMaindata = value; }
        }

        private IEventAggregator _events;
        private IAPIHelper _aPIHelper;
        public ExpenseCatView(IEventAggregator events , IAPIHelper aPIHelper)
        {

            InitializeComponent();
            _events = events;
            _aPIHelper = aPIHelper;
            _productsMaindata = new ObservableCollection<productVersionModel>();
            _ItemTranMaindata = new ObservableCollection<ItemTransactionModel>();
            BindingOperations.EnableCollectionSynchronization(ProductMainData, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(ItemTranMainData, _lockMutex1);
            ItemsTransation.ItemsSource = ItemTranMainData;
            PRODUCTS.ItemsSource = ProductMainData;
            assginParties();
            PRODUCTS.SelectedIndex = 0;

        }
        private void SaveButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            _ =  _events.PublishOnUIThreadAsync(new AddExpensePageViewEventModel());
        }

        public void assginParties()
        {

            _ = LoadDataAsync();

        }

        private void PARTIES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productVersionModel product = (productVersionModel)PRODUCTS.SelectedItem;
            if (product != null)
            {
                if (product.productName != null)
                {
                    NameText.Text = product.productName;
                    StockQuantity.Text = product.productQuntity.ToString();
                    assginTransaction(product.productNo);
                }
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

                _products = ProductVersionModelSqlite.readAll();
                foreach (productVersionModel c in _products)
                {
                    _productsMaindata.Add(c);
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
                _productsMaindata.Clear();
                foreach (productVersionModel c in _products.Where(s => s.productName.ToLower().Contains(key)).ToList())
                {
                    _productsMaindata.Add(c);
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
                _productsMaindata.Clear();
                foreach (productVersionModel c in _products)
                {
                    _productsMaindata.Add(c);
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

        private List<ItemTransactionModel> itemTransaction;
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
            foreach (ItemTransactionModel c in itemTransaction.Where(s => s.Name.ToLower().Contains(key)).ToList())
            {
                _ItemTranMaindata.Add(c);
            }

        }

        private void LoadTranDataAsync(int productID)
        {


            itemTransaction = ProductVersionModelSqlite.readAllTransaction(productID);
            foreach (ItemTransactionModel c in itemTransaction)
            {
                _ItemTranMaindata.Add(c);
            }

        }

        
    }
}
