using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TAN.EventModels;
using TAN.Helpers;
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
        public AddPartiesView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            closePage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
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
            var temp = appConfigSqlite.getData();
            var token = temp.adminToken;
            var data = new customerModel(1, customerName, 0, customerNumber, customerAddress, customerOP, TotalAmount);
            var ans = await _apiHelper.postCustomers(token, data);
            CustomerSqllite.addData(ans.data);
            
            _ = _events.PublishOnUIThreadAsync(new ClearChildShellView());
        }


    }
}
