using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TAN.EventModels;
using TAN.Helpers;
using TAN.PostRequest;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for SalePageView.xaml
    /// </summary>
    public partial class SalePageView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        private int _totalQTY;
        private float _finalAmount;
        private float _balance;
        private int _orderTypeGlobal;
        public SalePageView(IEventAggregator events, IAPIHelper aPIHelper, int orderType)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _orderTypeGlobal = orderType;
            //InvoiceDatePicker.Text = DateTime.Now.ToString();
            var tem = OrderTableSqlite.getInvoiceNo(1);
            _invoiceNO = tem == -1 ? 1 : tem + 1;
            AboveInVoiceNo.Text = _invoiceNO.ToString();
            InvoiceDatePicker.SelectedDate = DateTime.Now.Date;

            ITEMNO = 1;
            _finalAmount = 0;
            _totalQTY = 0;
            _balance = 0;
            ITEMS = new ObservableCollection<ItemdataModel>();
            saleDataGrid.ItemsSource = ITEMS;
            addRow();
            addRow();
            assognCombobox();
        }


        private List<PaymentTypeModel> _paymentTypeModels;
        private void assognCombobox()
        {
            _paymentTypeModels = PaymentTypeSqlite.readAll();

            foreach (PaymentTypeModel item in _paymentTypeModels)
            {
                ComboBoxPaymentType.Items.Add(item.paymentTypeName);
            }
            ComboBoxPaymentType.SelectedIndex = 0;
        }


        private int _ItemNo;

        public int ITEMNO
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }



        private ObservableCollection<ItemdataModel> _items;

        public ObservableCollection<ItemdataModel> ITEMS
        {
            get { return _items; }
            set { _items = value; }
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            addRow();
        }
        private void addRow()
        {
            var temp = new ItemdataModel(0, _ItemNo, "", 0, 0, 0
                );
            ITEMS.Add(temp);
            ITEMNO += 1;
        }

        private void ProductSuggestionTextBox_ProductSeleted(object sender, ProductDataEventArgs e)
        {
            int i = saleDataGrid.SelectedIndex;


            var row = saleDataGrid.CurrentItem as ItemdataModel;
            //Find in your collection index of selected row
            //Create a copy of this row
            var k = row.Clone() as ItemdataModel;
            //Here you can mofidy what you want
            k.ProductNo = e.SelectedProductData.productNo;
            k.ItemName = e.SelectedProductData.productName;
            k.Price = e.SelectedProductData.productPrice;
            k.ItemQuntity = 1;
            k.Amount = k.Price;
            //Replace object in you collection
            ITEMS[i] = k;
            if (i >= 1)
                addRow();
            //saleDataGrid.ItemsSource = null;
            //saleDataGrid.ItemsSource = ITEMS;
            _finalAmount -= row.Amount;
            _totalQTY -= row.ItemQuntity;

            _finalAmount += k.Amount;
            _totalQTY += k.ItemQuntity;
            _balance = _finalAmount;
            FinalAmount.Text = _finalAmount.ToString();
            TotalQty.Text = _totalQTY.ToString();
            BelowTotalTextBlock.Text = _finalAmount.ToString();
            BelowBalanceTextBlock.Text = _finalAmount.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            BelowBalanceTextBlock.Text = "0";
            BelowReceivedTextBox.Text = _balance.ToString();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            BelowBalanceTextBlock.Text = _balance.ToString();
            BelowReceivedTextBox.Text = string.Empty;
        }

        private customerModel _customerData;
        private void CustomerSuggestionTextBox_CustomerSeleted(object sender, CustomerDataEventArgs args)
        {
            _customerData = (customerModel)args.SelectedcustomerData.Clone();
        }
        private void CustomerSuggestionTextBox_AddPartySelected(object sender, AddCustomerEventArgs args)
        {
            _ = _events.PublishOnUIThreadAsync(new AddPartyEventModel());
        }


        //save button clicked
        private PlaceOrderPostRequest placeOrder;
        private int _invoiceNO;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {


            List<int> _prouctNo = new List<int>();
            List<int> _orderQuantitys = new List<int>();
            List<float> _orderSellingPrices = new List<float>();

            var lenItems = ITEMS.Count;
            foreach (var item in ITEMS)
            {
                if (item.ProductNo != 0)
                {
                    _prouctNo.Add(item.ProductNo);
                    _orderQuantitys.Add(item.ItemQuntity);
                    _orderSellingPrices.Add(item.Price);
                }

            }
            float tempReceivebal = float.Parse("0");
            if (BelowReceivedTextBox.Text != string.Empty)
            {
                tempReceivebal = float.Parse(BelowReceivedTextBox.Text);
            }
            string temporderdate = "";
            if (DateTime.Now.Date == InvoiceDatePicker.SelectedDate)
            {
                temporderdate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                temporderdate = Convert.ToDateTime(InvoiceDatePicker.SelectedDate).ToString("yyyy-MM-dd HH:mm:ss");
            }



            //assign request
            placeOrder = new PlaceOrderPostRequest();
            //payment
            placeOrder.payment.paymentType = _paymentTypeModels[ComboBoxPaymentType.SelectedIndex].paymentTypeId;
            placeOrder.payment.paymentDate = InvoiceDatePicker.SelectedDate.ToString();
            placeOrder.payment.paymentAmount = tempReceivebal;
            placeOrder.payment.remainingBalance = float.Parse(BelowBalanceTextBlock.Text.ToString());
            placeOrder.payment.TotalBalance = float.Parse(BelowTotalTextBlock.Text.ToString());
            //order
            placeOrder.order.orderDate = temporderdate;
            placeOrder.order.paymentDone = 1;
            placeOrder.order.customerID = _customerData.customerID;
            placeOrder.order.orderType = _orderTypeGlobal;
            placeOrder.order.InvoiceNo = int.Parse(AboveInVoiceNo.Text);
            //relation
            placeOrder.productNos = _prouctNo;
            placeOrder.orderQuantitys = _orderQuantitys;
            placeOrder.orderSellingPrices = _orderSellingPrices;


            //call request
            var appConfigtemp = appConfigSqlite.getData();
            string token = appConfigtemp.adminToken;
            var result = await _apiHelper.postPlaceOrder1(token, placeOrder);
            paymentSqlite.addData(result.data.payment);
            OrderTableSqlite.addData(result.data.order);

            appConfigtemp.apiVersion = result.apiVersion;
            appConfigSqlite.editData(appConfigtemp);

            var relation = result.data.orderproductrelation;

            for (int i = 0; i < relation.productNo.Count; i++)
            {
                orderProductRelationModel relModel = new orderProductRelationModel(relation.orderID, relation.productNo[i],
                    relation.orderQuantity[i], relation.orderSellingPrice[i]);
                orderProductRelationModelSqlite.addData(relModel);
            }
            //editing customer amount locally
            //ustomerModel customer = CustomerSqllite.getSingleDataByID(_customerData.customerID);
            customerModel customer = (customerModel)_customerData.Clone();
            customer.TotalAmount += placeOrder.payment.remainingBalance;
            CustomerSqllite.UpdateByID(customer);
            //Update Item Quntity Locally
            for (int i = 0; i < relation.productNo.Count; i++)
            {
                productVersionModel productVersionModel = ProductVersionModelSqlite.getSingleDataByID(relation.productNo[i]);

                if (_orderTypeGlobal == 1)
                {
                    productVersionModel.productQuntity -= placeOrder.orderQuantitys[i];
                }
                else if(_orderTypeGlobal == 2)
                {
                    productVersionModel.productQuntity += placeOrder.orderQuantitys[i];
                }
                else if (_orderTypeGlobal == 5)
                {
                    productVersionModel.productQuntity += placeOrder.orderQuantitys[i];
                }
                else if (_orderTypeGlobal == 6)
                {
                    productVersionModel.productQuntity -= placeOrder.orderQuantitys[i];
                }





                productVersionModel.salePrice = placeOrder.orderSellingPrices[i];
                ProductVersionModelSqlite.UpdateProductByID(productVersionModel);
            }

            _ = _events.PublishOnUIThreadAsync(new ClearSalePageEventModel());
        }

        //close button clicked
        private void Button_Click_1(object sender, RoutedEventArgs e)
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
                    _ = _events.PublishOnUIThreadAsync(new ClearSalePageEventModel());
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }





        private void BelowReceivedTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (BelowReceivedTextBox.Text != string.Empty)
            {
                float re = float.Parse(BelowReceivedTextBox.Text);
                float ba = float.Parse(BelowBalanceTextBlock.Text);
                float to = float.Parse(BelowTotalTextBlock.Text);
                BelowBalanceTextBlock.Text = (to - re).ToString();
            }
            else
            {
                BelowBalanceTextBlock.Text = BelowTotalTextBlock.Text;
            }
        }
        /*
        private void saleDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            
                //        if (index != -1)
                //        {
                            
                //            var row = ITEMS[index] as ItemdataModel;
                //            var k = row.Clone() as ItemdataModel;
                //            ITEMS[index] = k;

                //            if (coIndex == 2)
                //            {
                //                _finalAmount -= firstItemData.Amount;
                //                _totalQTY -= firstItemData.ItemQuntity;
                //                _totalQTY += k.ItemQuntity;
                //            }
                //            if (coIndex == 3)
                //            {
                //                _finalAmount -= firstItemData.Amount;
                //            }
                //            if (coIndex == 4)
                //            {
                //                _finalAmount -= firstItemData.Amount;
                //            }


                //            _finalAmount += k.Amount;
                           
                //            _balance = _finalAmount;
                //            FinalAmount.Text = _finalAmount.ToString();
                //            TotalQty.Text = _totalQTY.ToString();
                //            BelowTotalTextBlock.Text = _finalAmount.ToString();

                //            if (BelowReceivedCheckBox.IsChecked == false)
                //            {
                //                BelowBalanceTextBlock.Text = _finalAmount.ToString();
                //            }
                //            else
                //            {
                //                float temp34 = _finalAmount - float.Parse(BelowReceivedTextBox.Text);
                //                BelowBalanceTextBlock.Text = temp34.ToString();
                //            }
                //            index = -1;
                //            coIndex = -1; 
                //        }
                        
                //            var temp
                //             = (ItemdataModel)saleDataGrid.CurrentItem;
                //if (temp != null)
                //{
                //    firstItemData = (ItemdataModel)temp.Clone();
                //}
                            
                        
                    
        }*/
        //int index = -1;
        int coIndex = -1;

        ItemdataModel firstItemData;
        private void saleDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (saleDataGrid.CurrentColumn.DisplayIndex == 2
                || saleDataGrid.CurrentColumn.DisplayIndex == 3
                || saleDataGrid.CurrentColumn.DisplayIndex == 4)
            {
                coIndex = saleDataGrid.CurrentColumn.DisplayIndex;
                var row = saleDataGrid.CurrentItem as ItemdataModel;
                float newAmount = float.Parse(getTextBoxInSaleDataGrid("textBo1xAmount", row.ItemNO));
                float newPrice = float.Parse(getTextBoxInSaleDataGrid("textBo1xPrice", row.ItemNO));
                int newQuantity = int.Parse(getTextBoxInSaleDataGrid("textBo1xQuantity", row.ItemNO));


                float ansPrice = -934;
                float ansAmount = -934;
                if (coIndex == 2)
                {
                    _finalAmount -= newAmount;
                    _totalQTY -= firstItemData.ItemQuntity;
                    _totalQTY += newQuantity;
                    _finalAmount += (newQuantity * newPrice);
                    ansAmount = (newPrice * newQuantity);
                }
                else if (coIndex == 3)
                {
                    _finalAmount -= newAmount;
                    _finalAmount += (newQuantity * newPrice);
                    ansAmount = (newQuantity * newPrice);
                }
                else if (coIndex == 4)
                {
                    if (newAmount != 0)
                        ansPrice = newAmount / newQuantity;
                    else
                        ansPrice = 0;
                    _finalAmount -= firstItemData.Amount;
                    _finalAmount += newAmount;

                }



                if (ansAmount != -934)
                {
                    setTextBoxInSaleDataGrid("textBo1xAmount", row.ItemNO, ansAmount.ToString());
                }
                if (ansPrice != -934)
                {
                    setTextBoxInSaleDataGrid("textBo1xPrice", row.ItemNO, ansPrice.ToString());
                }



                _balance = _finalAmount;
                FinalAmount.Text = _finalAmount.ToString();
                TotalQty.Text = _totalQTY.ToString();
                BelowTotalTextBlock.Text = _finalAmount.ToString();

                if (BelowReceivedCheckBox.IsChecked == false)
                {
                    BelowBalanceTextBlock.Text = _finalAmount.ToString();
                }
                else
                {
                    float temp34 = _finalAmount - float.Parse(BelowReceivedTextBox.Text);
                    BelowBalanceTextBlock.Text = temp34.ToString();
                }
                if (newQuantity == 0)
                {
                    ITEMS[row.ItemNO - 1].ItemQuntity = newQuantity;
                }
                if (newAmount == 0)
                {
                    ITEMS[row.ItemNO - 1].Amount = newAmount;
                }
                firstItemData = (ItemdataModel)ITEMS[row.ItemNO - 1].Clone();

            }
            /*
            //if (saleDataGrid.CurrentColumn.DisplayIndex == 2)
            //{
            //    var row = saleDataGrid.CurrentItem as ItemdataModel;
            //    //var k = row.Clone() as ItemdataModel;
            //    index = row.ItemNO - 1;
            //    //k.Amount = k.ItemQuntity * k.Price;

            //    ITEMS[index].Amount = row.ItemQuntity * row.Price;
            //    coIndex = 2;
            //}
            //if (saleDataGrid.CurrentColumn.DisplayIndex == 3)
            //{
            //    var row = saleDataGrid.CurrentItem as ItemdataModel;
            //    //var k = row.Clone() as ItemdataModel;
            //    index = row.ItemNO - 1;
            //    //k.Amount = k.ItemQuntity * k.Price;

            //    ITEMS[index].Amount = row.ItemQuntity * row.Price;
            //    coIndex = 3;
            //}
            //if (saleDataGrid.CurrentColumn.DisplayIndex == 4)
            //{
            //    var row = saleDataGrid.CurrentItem as ItemdataModel;
            //    //var k = row.Clone() as ItemdataModel;
            //    index = row.ItemNO - 1;
            //    //k.Amount = k.ItemQuntity * k.Price;

            //    ITEMS[index].Price = row.Amount/ row.ItemQuntity ;
            //    coIndex = 4;
            //}
            */
        }


        private void setTextBoxInSaleDataGrid(string NameofTextBox, int n, string data)
        {
            IEnumerable<TextBox> textboxes = saleDataGrid.FindAllVisualDescendants()
                .Where(elt => elt.Name == NameofTextBox)
                .OfType<TextBox>()
                .Skip(n - 1)
                .Take(1);
            textboxes.ElementAt(0).Text = data;
        }
        private string getTextBoxInSaleDataGrid(string NameofTextBox, int n)
        {
            IEnumerable<TextBox> textboxes = saleDataGrid.FindAllVisualDescendants()
                .Where(elt => elt.Name == NameofTextBox)
                .OfType<TextBox>()
                .Skip(n - 1)
                .Take(1);
            string result = textboxes.ElementAt(0).Text;
            if (result == null || result == "")
            {
                return "0";
            }
            return result;

        }
        int fork = -1;
        int forkrow = -1;
        private void saleDataGrid_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

            if (fork != saleDataGrid.CurrentColumn.DisplayIndex)
            {
                var temp = saleDataGrid.CurrentItem as ItemdataModel;
                firstItemData = (ItemdataModel)ITEMS[temp.ItemNO - 1].Clone();
                fork = saleDataGrid.CurrentColumn.DisplayIndex;
            }
            if (forkrow != saleDataGrid.Items.IndexOf(saleDataGrid.CurrentItem))
            {
                var temp = saleDataGrid.CurrentItem as ItemdataModel;
                firstItemData = (ItemdataModel)ITEMS[temp.ItemNO - 1].Clone();
                forkrow = saleDataGrid.Items.IndexOf(saleDataGrid.CurrentItem);
            }

        }

        
    }
}
