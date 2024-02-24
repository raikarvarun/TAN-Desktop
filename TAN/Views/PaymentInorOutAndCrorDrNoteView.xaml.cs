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
    /// Interaction logic for PaymentInorOutAndCrorDrNote.xaml
    /// </summary>
    public partial class PaymentInorOutAndCrorDrNoteView : UserControl
    {

        private static List<PaymentInModel> _sales;
        private object _lockMutex = new object();
        private ObservableCollection<PaymentInModel> _saleInvoiceModel;
        public ObservableCollection<PaymentInModel> SaleInvoiceModel
        {
            get { return _saleInvoiceModel; }
            set { _saleInvoiceModel = value; }
        }
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        private int _orderTypeGlobal;

        public PaymentInorOutAndCrorDrNoteView(IEventAggregator events, IAPIHelper aPIHelper, int orderType)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _orderTypeGlobal = orderType;

            if (orderType == 3)
            {
                AddButtonTextBlock.Text = "Add Payment-In";
            }
            else if (orderType == 4)
            {
                AddButtonTextBlock.Text = "Add Payment-Out";
            }
            else if (orderType == 5)
            {
                AddButtonTextBlock.Text = "Add Credit Note";
            }
            else if (orderType == 6)
            {
                AddButtonTextBlock.Text = "Add Debit Note";
            }

            _saleInvoiceModel = new ObservableCollection<PaymentInModel>();
            BindingOperations.EnableCollectionSynchronization(SaleInvoiceModel, _lockMutex);
            TransactionDatagrid.ItemsSource = SaleInvoiceModel;

            assginSales();
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_orderTypeGlobal == 5 || _orderTypeGlobal == 6)
                _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(_orderTypeGlobal, 1, null));
            else
                _events.PublishOnUIThreadAsync(new PaymentInEventModel(_orderTypeGlobal));
        }



        public void assginSales()
        {

            _ = LoadDataAsync();

        }
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {

                _sales = OrderTableSqlite.getInvoicesByPaymentInModel(_orderTypeGlobal); ;
                foreach (PaymentInModel c in _sales)
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
                foreach (PaymentInModel c in _sales)
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
            foreach (PaymentInModel c in _sales.Where(s => s.PartyName.ToLower().Contains(key)).ToList())
            {
                _saleInvoiceModel.Add(c);
            }

        }
    }
}
