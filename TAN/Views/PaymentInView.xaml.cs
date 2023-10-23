
using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows.Controls;
using TAN.EventModels;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PaymentInView.xaml
    /// </summary>
    public partial class PaymentInView : UserControl
    {
        private IEventAggregator _events;
        public PaymentInView(IEventAggregator events)
        {
            InitializeComponent();
            InitializeDataGridView();
            _events = events;
        }
        private void InitializeDataGridView()
        {
            _saleInvoiceModel = OrderTableSqlite.getInvoicesByPaymentInModel(3);
            PaymentInDatagrid.ItemsSource = _saleInvoiceModel;
        }

        private List<PaymentInModel> _saleInvoiceModel;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _ = _events.PublishOnUIThreadAsync(new PaymentInEventModel());
        }
    }
}
