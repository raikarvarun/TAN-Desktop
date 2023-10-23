using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for AddBankAccountView.xaml
    /// </summary>
    public partial class AddBankAccountView : UserControl
    {

        public delegate void TEmpfun();
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;

        private TEmpfun _changeCurrentViewtoItems;
        public AddBankAccountView(IEventAggregator events, IAPIHelper aPIHelper, TEmpfun re)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _changeCurrentViewtoItems = re;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _changeCurrentViewtoItems();
        }

        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            string name = AccountDisplayName.Text;

            var temp = appConfigSqlite.getData();
            var token = temp.adminToken;

            var data = new PaymentTypeModel(1,
                "BANKS",
                name
                );

            var ans = await _apiHelper.postPaymentType(token, data);
            PaymentTypeSqlite.addData(ans.data);
            _changeCurrentViewtoItems();
        }
    }
}
