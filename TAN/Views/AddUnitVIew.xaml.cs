using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using TAN.EventModels;
using TAN.Helpers;
using TAN.Notification.Utils;
using TAN.Notification;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for AddItemView.xaml
    /// </summary>
    public partial class AddUnitVIew : UserControl
    {
        public delegate void TEmpfun();
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;

        private TEmpfun _changeCurrentViewtoItems;
        public AddUnitVIew(IEventAggregator events, IAPIHelper aPIHelper, TEmpfun re)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _changeCurrentViewtoItems = re;
        }


        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            var unitName = UnitName.Text;
            var shortName = ShortName.Text;

            if(unitName == "" )
            {
                var notificationManager = new NotificationManager();

                notificationManager.Show(new NotificationContent
                {
                    Title = "Please Enter Unit Name",

                    Type = NotificationType.Error
                });
                return;
            }
            if( shortName == "")
            {
                var notificationManager = new NotificationManager();

                notificationManager.Show(new NotificationContent
                {
                    Title = "Please Enter Short Name",

                    Type = NotificationType.Error
                });
                return;
            }

            var appConfigtemp =  appConfigSqlite.getData();
            var token = appConfigtemp.adminToken;

            var data = new ItemUnitModel(0,unitName , shortName);

            var ans = await _apiHelper.postItemUnit1(token, data);
            ItemUnitSqllite.addData(ans.data);

            appConfigtemp.apiVersion = ans.apiVersion;
            appConfigSqlite.editData(appConfigtemp);

            _changeCurrentViewtoItems();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _changeCurrentViewtoItems();
        }

        private void SaveNewButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
