using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using TAN.Helpers;
using TAN.Notification.Utils;
using TAN.Notification;

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

            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;

            var data = new PaymentTypeModel(1,
                "BANKS",
                name
                );

            var ans = await _apiHelper.postPaymentType(token, data);
            PaymentTypeSqlite.addData(ans.data);
            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "Add Bank Account Sucessfully",

                Type = NotificationType.Success
            });

            _changeCurrentViewtoItems();
        }
    }
}
