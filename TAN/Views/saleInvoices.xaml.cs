using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for saleInvoices.xaml
    /// </summary>
    public partial class saleInvoices : UserControl
    {
        private static List<SaleInvoiceModel> _sales;
        private object _lockMutex = new object();
        private ObservableCollection<SaleInvoiceModel> _saleInvoiceModel;
        public ObservableCollection<SaleInvoiceModel> SaleInvoiceModel
        {
            get { return _saleInvoiceModel; }
            set { _saleInvoiceModel = value; }
        }
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        private int _orderTypeGlobal;

        public saleInvoices(IEventAggregator events, IAPIHelper aPIHelper, int orderType)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _orderTypeGlobal = orderType;

            if (orderType == 1)
            {
                AddButton.Text = "Add Sale";
            }
            else
            {
                AddButton.Text = "Add Purchase";
            }



            _saleInvoiceModel = new ObservableCollection<SaleInvoiceModel>();
            BindingOperations.EnableCollectionSynchronization(SaleInvoiceModel, _lockMutex);
            SaleDatagrid.ItemsSource = SaleInvoiceModel;

            assginSales();
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(_orderTypeGlobal, 1, null));
        }



        public void assginSales()
        {

            _ = LoadDataAsync();

        }
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {

                _sales = OrderTableSqlite.getInvoices(_orderTypeGlobal);
                foreach (SaleInvoiceModel c in _sales)
                {
                    _saleInvoiceModel.Add(c);
                }
            });
        }



        private void CloseSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox1.Clear();

        }



        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchKey = searchTextBox1.Text.ToLower();
            if (searchKey == "")
            {
                CloseSearchButton1.Visibility = Visibility.Collapsed;
                ReassginWholeData();
            }
            else
            {
                CloseSearchButton1.Visibility = Visibility.Visible;
                SearchData1(searchKey);
            }
        }


        private void ReassginWholeData()
        {
            _ = ReassginWholeDataAsync();
        }
        private Task ReassginWholeDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                _saleInvoiceModel.Clear();
                foreach (SaleInvoiceModel c in _sales)
                {
                    _saleInvoiceModel.Add(c);
                }
            });
        }

        private void SearchData1(string key)
        {
            SearchData1Async(key);
        }
        private void SearchData1Async(string key)
        {

            _saleInvoiceModel.Clear();
            foreach (SaleInvoiceModel c in _sales.Where(s => s.PartyName.ToLower().Contains(key)).ToList())
            {
                _saleInvoiceModel.Add(c);
            }

        }


    }
}
