using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for AddItemView.xaml
    /// </summary>
    public partial class AddItemView : UserControl
    {
        public delegate void TEmpfun();
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;

        private TEmpfun _changeCurrentViewtoItems;
        public AddItemView(IEventAggregator events, IAPIHelper aPIHelper, TEmpfun re)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _changeCurrentViewtoItems = re;
            var boxData = ItemUnitSqllite.readAll();
            
            for(int i = 0; i < boxData.Count; i++)
            {
                var item = boxData[i].FullName +" ("+boxData[i].ShortName + ")";
                UnitBox.Items.Add(item);
            }

        }


        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            var productName = ItemName.Text;
            float atprice = 0;
            if (Atprice.Text.Length > 0)
            {
                atprice = float.Parse(Atprice.Text);
            }

            float salePrice = 0;
            if (SalePrice.Text.Length > 0)
            {
                salePrice = float.Parse(SalePrice.Text);
            }
            int productOQ = 0;
            if (Amount.Text.Length > 0)
            {
                productOQ = int.Parse(Amount.Text);
            }

            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;

            var data = new productVersionModel(1,
                "null",
                atprice,
                productOQ,
                "null",
                productName,
                productOQ,
                atprice,
                salePrice,
                0);

            var ans = await _apiHelper.postProducts(token, data);
            ProductVersionModelSqlite.addData(ans.data);
            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            _changeCurrentViewtoItems();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _changeCurrentViewtoItems();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

       
    }
}
