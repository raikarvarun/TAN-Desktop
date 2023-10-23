using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows.Controls;


namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for saleInvoices.xaml
    /// </summary>
    public partial class saleInvoices : UserControl
    {
        public saleInvoices()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {


            _saleInvoiceModel = OrderTableSqlite.getInvoices(1);
            SaleDatagrid.ItemsSource = _saleInvoiceModel;
        }

        private List<SaleInvoiceModel> _saleInvoiceModel;

    }
}
