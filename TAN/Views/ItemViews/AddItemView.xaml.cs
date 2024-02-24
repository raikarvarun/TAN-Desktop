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
    public partial class AddItemView : UserControl
    {
        public delegate void TEmpfun();
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        private int _whichMode;
        private productVersionModel _selectedProduct;
        private TEmpfun _changeCurrentViewtoItems;
        public AddItemView(IEventAggregator events, IAPIHelper aPIHelper, TEmpfun re, int whichMode, productVersionModel selectedProduct)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _changeCurrentViewtoItems = re;
            _whichMode = whichMode;
            _selectedProduct = selectedProduct;
            var boxData = ItemUnitSqllite.readAll();

            for (int i = 0; i < boxData.Count; i++)
            {
                var item = boxData[i].FullName + " (" + boxData[i].ShortName + ")";
                UnitBox.Items.Add(item);
            }

            if (whichMode == 1)
            {

            }
            else
            {
                ItemTitle.Text = "Edit Item";
                SaveNewButton.Content = "Delete";
                SaveButton.Content = "Update";
                ItemName.SetText = _selectedProduct.productName;
                Atprice.SetText = _selectedProduct.atprice.ToString();
                SalePrice.SetText = _selectedProduct.salePrice.ToString();
                Amount.SetText = _selectedProduct.atprice.ToString();
                PurchasePrice.SetText = _selectedProduct.purchasePrice.ToString();

            }

        }


        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_whichMode == 1)
            {
                SaveButtonLogic();
            }
            else
            {
                UpdateButtonLogic();
            }
        }
        private void SaveNewButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_whichMode == 1)
            {
                SaveNewButtonLogic();
            }
            else
            {

            }

        }
        private async void SaveButtonLogic()
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
            float purchasePrice = 0;
            if (PurchasePrice.Text.Length > 0)
            {
                purchasePrice = float.Parse(PurchasePrice.Text);
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
                purchasePrice);

            var ans = await _apiHelper.postProducts(token, data);
            ProductVersionModelSqlite.addData(ans.data);
            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "New Item Save Sucessfully",

                Type = NotificationType.Success
            });

            _changeCurrentViewtoItems();
        }
        private async void SaveNewButtonLogic()
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
            float purchasePrice = 0;
            if (PurchasePrice.Text.Length > 0)
            {
                purchasePrice = float.Parse(PurchasePrice.Text);
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
                purchasePrice);

            var ans = await _apiHelper.postProducts(token, data);
            ProductVersionModelSqlite.addData(ans.data);
            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "New Item Save Sucessfully",

                Type = NotificationType.Success
            });

            ItemName.Text = "";
            Atprice.Text = "";
            SalePrice.Text = "";
            Amount.Text = "";
            PurchasePrice.Text = "";
        }
        private async void UpdateButtonLogic()
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
            float purchasePrice = 0;
            if (PurchasePrice.Text.Length > 0)
            {
                purchasePrice = float.Parse(PurchasePrice.Text);
            }
            var admin = AdminTableSqlite.getAdminData();
            var token = admin.adminToken;

            var data = new productVersionModel(_selectedProduct.productNo,
                "null",
                atprice,
                productOQ,
                "null",
                productName,
                productOQ,
                atprice,
                salePrice,
                purchasePrice);

            var ans = await _apiHelper.editProduct(token, data);
            ProductVersionModelSqlite.UpdateProductByID(ans.data);
            appConfigSqlite.editData(ans.apiVersion[0].appconfigName, ans.apiVersion[0].appconfigVersion);

            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "Item Updated Sucessfully",

                Type = NotificationType.Success
            });

            _changeCurrentViewtoItems();
        }
        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            _changeCurrentViewtoItems();
        }




    }
}
