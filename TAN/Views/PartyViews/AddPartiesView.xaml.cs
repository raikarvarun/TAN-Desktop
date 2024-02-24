using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TAN.EventModels;
using TAN.Helpers;
using TAN.Notification.Utils;
using TAN.Notification;
using TAN.ViewModels;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for AddParties.xaml
    /// </summary>
    public partial class AddPartiesView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        private int _whichMode;
        private customerModel _mainCustomerData;
        public AddPartiesView(IEventAggregator events, IAPIHelper aPIHelper, int whichMode, customerModel mainCustomerData)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _whichMode = whichMode;
            _mainCustomerData = mainCustomerData;
            if (_whichMode == 1)
            {
                PageTittle.Text = "Add Party";

            }
            else if (_whichMode == 2)
            {
                PageTittle.Text = "Edit Party";
                SaveNewButton.Content = "Delete";
                SaveButton.Content = "Update";
                PartyName.SetText = mainCustomerData.customerName;
                PhoneNumber.SetText = mainCustomerData.customerMobile;
                Amount.SetText = mainCustomerData.TotalAmount.ToString();
                Address.SetText = mainCustomerData.customerAddress;

            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            closePage();
        }


        private void closePage()
        {
            string msgtext = "Current changes will be discarded. Do you want to continue?";
            string txt = "TAN NIBM";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.OK:
                    _ = _events.PublishOnUIThreadAsync(new ClearChildShellView());
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }
        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_whichMode == 1)
                SaveData();
            else
                UpdateData();
        }
        private void SaveNewButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_whichMode == 1)
                SaveNewData();
            else
                DeleteData();
        }
        private async void SaveData()
        {
            var customerName = PartyName.Text;
            var customerNumber = PhoneNumber.Text;
            float customerOP = float.Parse("0");
            if (Amount.Text.Length > 0)
            {
                customerOP = float.Parse(Amount.Text);
            }

            var customerAddress = Address.Text;
            float TotalAmount = customerOP;
            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;
            var data = new customerModel(1, customerName, 0, customerNumber, customerAddress, customerOP, TotalAmount);
            var ans = await _apiHelper.postCustomers(token, data);
            CustomerSqllite.addData(ans.data);


            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = "New Party Save Sucessfully",

                Type = NotificationType.Success
            });
            _ = _events.PublishOnUIThreadAsync(new ClearChildShellView());
        }


        private void DeleteData()
        {

        }
        private async void SaveNewData()
        {
            var customerName = PartyName.Text;
            var customerNumber = PhoneNumber.Text;
            float customerOP = float.Parse("0");
            if (Amount.Text.Length > 0)
            {
                customerOP = float.Parse(Amount.Text);
            }

            var customerAddress = Address.Text;
            float TotalAmount = customerOP;
            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;
            var data = new customerModel(1, customerName, 0, customerNumber, customerAddress, customerOP, TotalAmount);
            var ans = await _apiHelper.postCustomers(token, data);
            CustomerSqllite.addData(ans.data);


            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = "New Party Save Sucessfully",

                Type = NotificationType.Success
            });
            PartyName.SetText = "";
            PhoneNumber.SetText = "";
            Amount.SetText = "";
            Address.SetText = "";
        }
        private async void UpdateData()
        {
            var customerName = PartyName.Text;
            var customerNumber = PhoneNumber.Text;
            float customerOP = float.Parse("0");
            if (Amount.Text.Length > 0)
            {
                customerOP = float.Parse(Amount.Text);
            }

            var customerAddress = Address.Text;
            float TotalAmount = customerOP;
            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;
            var data = new customerModel(_mainCustomerData.customerID, customerName, 0, customerNumber, customerAddress, customerOP, TotalAmount);
            var ans = await _apiHelper.editCustomer(token, data);
            CustomerSqllite.UpdateByID(ans.data);


            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = "Party Updated Sucessfully",

                Type = NotificationType.Success
            });
            _ = _events.PublishOnUIThreadAsync(new ClearChildShellView());
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            closePage();
        }
    }
}
