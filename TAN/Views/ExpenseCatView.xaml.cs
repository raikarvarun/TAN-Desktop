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
        private static List<ExpenseCategoryModel> _expenseCats;
        private ObservableCollection<ExpenseCategoryModel> _expenseCatMaindata;
        private ObservableCollection<ItemTransactionModel> _ItemTranMaindata;
        private object _lockMutex = new object();
        private object _lockMutex1 = new object();
        private int currentProdcutID;
        public ObservableCollection<ExpenseCategoryModel> ExpenseCatMainData
        {
            get { return _expenseCatMaindata; }
            set { _expenseCatMaindata = value; }
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
            _expenseCatMaindata = new ObservableCollection<ExpenseCategoryModel>();
            _ItemTranMaindata = new ObservableCollection<ItemTransactionModel>();
            BindingOperations.EnableCollectionSynchronization(ExpenseCatMainData, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(ItemTranMainData, _lockMutex1);
            //ItemsTransation.ItemsSource = ItemTranMainData;
            EXPENSECATDATAGRID.ItemsSource = ExpenseCatMainData;
            assginParties();
            EXPENSECATDATAGRID.SelectedIndex = 0;

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
            ExpenseCategoryModel data = (ExpenseCategoryModel)EXPENSECATDATAGRID.SelectedItem;
            if (data != null)
            {
                if (data.ExpenseCategaryName != null)
                {
                    NameText.Text = data.ExpenseCategaryName;
                    
                    //assginTransaction(product.productNo);
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

                _expenseCats = ExpenseCategorySqllite.readAll();
                foreach (ExpenseCategoryModel c in _expenseCats)
                {
                    _expenseCatMaindata.Add(c);
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
                _expenseCatMaindata.Clear();
                foreach (ExpenseCategoryModel c in _expenseCats.Where(s => s.ExpenseCategaryName.ToLower().Contains(key)).ToList())
                {
                    _expenseCatMaindata.Add(c);
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
                _expenseCatMaindata.Clear();
                foreach (ExpenseCategoryModel c in _expenseCats)
                {
                    _expenseCatMaindata.Add(c);
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
