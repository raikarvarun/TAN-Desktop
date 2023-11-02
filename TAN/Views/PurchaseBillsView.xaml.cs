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
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PurchaseBillsView.xaml
    /// </summary>
    public partial class PurchaseBillsView : UserControl
    {
        
        


        private static List<SaleInvoiceModel> _purchases;
        private object _lockMutex = new object();
        private ObservableCollection<SaleInvoiceModel> _purchaseInvoiceModel;
        public ObservableCollection<SaleInvoiceModel> PurchaseInvoiceModel
        {
            get { return _purchaseInvoiceModel; }
            set { _purchaseInvoiceModel = value; }
        }
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;

        public PurchaseBillsView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _purchaseInvoiceModel = new ObservableCollection<SaleInvoiceModel>();
            BindingOperations.EnableCollectionSynchronization(PurchaseInvoiceModel, _lockMutex);
            PurchaseDatagrid.ItemsSource = PurchaseInvoiceModel;

            assginSales();
        }

        private void AddSaleButtonClicked(object sender, RoutedEventArgs e)
        {
            _events.PublishOnUIThreadAsync(new ShowPurchasePageEventMOdel());
        }



        public void assginSales()
        {

            _ = LoadDataAsync();

        }
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {

                _purchases = OrderTableSqlite.getInvoices(2);
                foreach (SaleInvoiceModel c in _purchases)
                {
                    _purchaseInvoiceModel.Add(c);
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
                _purchaseInvoiceModel.Clear();
                foreach (SaleInvoiceModel c in _purchases)
                {
                    _purchaseInvoiceModel.Add(c);
                }
            });
        }

        private void SearchData1(string key)
        {
            SearchData1Async(key);
        }
        private void SearchData1Async(string key)
        {

            _purchaseInvoiceModel.Clear();
            foreach (SaleInvoiceModel c in _purchases.Where(s => s.PartyName.ToLower().Contains(key)).ToList())
            {
                _purchaseInvoiceModel.Add(c);
            }

        }
    }
}
