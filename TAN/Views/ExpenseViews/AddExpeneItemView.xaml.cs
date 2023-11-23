using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System;
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
    public partial class AddExpeneItemView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public AddExpeneItemView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            closePage();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            closePage();
        }
        private void closePage()
        {
            _ = _events.PublishOnUIThreadAsync(new RemoveExpenseItemEventModel());
                    
        }
        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            var expenseitemName = ExpenseitemTextBox.Text;
            var price = Double.Parse( PriceTextBox.Text);


            var data = new ExpenseItemModel(0, expenseitemName, price);

            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;

            var ans = await _apiHelper.postExpenseItem(token, data);

            ExpenseItemSqllite.addData(ans.data);

            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            closePage();
        }


    }
}
