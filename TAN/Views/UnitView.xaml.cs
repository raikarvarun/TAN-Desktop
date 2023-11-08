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

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    /// 


    public partial class UnitView : UserControl
    {
        private static List<ItemUnitModel> _units;
        private ObservableCollection<ItemUnitModel> _unitsMaindata;
        
        private object _lockMutex = new object();
        
        private int currentProdcutID;
        public ObservableCollection<ItemUnitModel> UnitsMainData
        {
            get { return _unitsMaindata; }
            set { _unitsMaindata = value; }
        }
        
        public delegate void TEmpfun();
        private TEmpfun AddItemFun;
        public UnitView(IEventAggregator events, TEmpfun tempfun)
        {
            AddItemFun = tempfun;
            InitializeComponent();
            _unitsMaindata = new ObservableCollection<ItemUnitModel>();
            
            BindingOperations.EnableCollectionSynchronization(UnitsMainData, _lockMutex);
           
            
            PRODUCTS.ItemsSource = UnitsMainData;
            assginParties();
            PRODUCTS.SelectedIndex = 0;

        }
        private void SaveButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            AddItemFun();
        }

        public void assginParties()
        {

            _ = LoadDataAsync();

        }

        private void PARTIES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemUnitModel product = (ItemUnitModel)PRODUCTS.SelectedItem;
            if (product != null)
            {
                if (product.FullName != null)
                {
                    NameText.Text = product.FullName;
                    
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

                _units = ItemUnitSqllite.readAll();
                foreach (ItemUnitModel c in _units)
                {
                    _unitsMaindata.Add(c);
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
                _unitsMaindata.Clear();
                foreach (ItemUnitModel c in _units.Where(s => s.FullName.ToLower().Contains(key)).ToList())
                {
                    _unitsMaindata.Add(c);
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
                _unitsMaindata.Clear();
                foreach (ItemUnitModel c in _units)
                {
                    _unitsMaindata.Add(c);
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
