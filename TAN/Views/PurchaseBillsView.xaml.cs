using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PurchaseBillsView.xaml
    /// </summary>
    public partial class PurchaseBillsView : UserControl
    {
        public PurchaseBillsView()
        {
            InitializeComponent();
            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {


            _purchaseInvoiceModel = OrderTableSqlite.getInvoices(2);
            PurchaseDatagrid.ItemsSource = _purchaseInvoiceModel;
        }

        private List<SaleInvoiceModel> _purchaseInvoiceModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
