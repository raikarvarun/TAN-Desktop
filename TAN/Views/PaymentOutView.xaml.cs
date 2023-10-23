using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PaymentOutView.xaml
    /// </summary>
    public partial class PaymentOutView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public PaymentOutView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _apiHelper = aPIHelper;
            _events = events;
            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {
            _saleInvoiceModel = OrderTableSqlite.getInvoicesByPaymentInModel(4);
            PaymentOutDatagrid.ItemsSource = _saleInvoiceModel;
        }

        private List<PaymentInModel> _saleInvoiceModel;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = _events.PublishOnUIThreadAsync(new PaymentOutEventModel());
        }
    }
}
