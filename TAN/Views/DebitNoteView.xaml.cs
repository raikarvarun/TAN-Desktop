using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows.Controls;
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for DebitNoteView.xaml
    /// </summary>
    public partial class DebitNoteView : UserControl
    {
        public DebitNoteView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            InitializeDataGridView();
            _events = events;
            _apiHelper = aPIHelper;
        }
        private void InitializeDataGridView()
        {
            _saleInvoiceModel = OrderTableSqlite.getInvoicesByPaymentInModel(6);
            DebitNoteDataGrid.ItemsSource = _saleInvoiceModel;
        }

        private List<PaymentInModel> _saleInvoiceModel;
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _ = _events.PublishOnUIThreadAsync(new addDebitNotePageViewEventModel());
        }
    }
}
