using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for CommanNavigationView.xaml
    /// </summary>
    public partial class CommanNavigationView : UserControl
    {

        public CommanNavigationView()
        {
            InitializeComponent();

            List<float> list = OrderTableSqlite.getSalesPaidUnpaidData();
            PaidAmount.Text =  "₹ " + DisplayIndianCurrency(list[0].ToString());
            UnPaidAmount.Text = "₹ " + DisplayIndianCurrency(list[1].ToString());
            TotalPaidUnpaidAmount.Text = "₹ " + DisplayIndianCurrency(list[2].ToString());


        }
        private string DisplayIndianCurrency(string data)
        {
            decimal parsed = decimal.Parse(data, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            hindi.NumberFormat.CurrencySymbol = "";
            string text = string.Format(hindi, "{0:c0}", parsed);
            return text;
        }


    }
}
