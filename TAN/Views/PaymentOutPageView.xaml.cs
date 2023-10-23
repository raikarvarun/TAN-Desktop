using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TAN.EventModels;
using TAN.Helpers;
using TAN.PostRequest;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for PaymentOutPageView.xaml
    /// </summary>
    public partial class PaymentOutPageView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        private int _invoiceNO;
        private bool _hideOrUnhide;
        public PaymentOutPageView(IEventAggregator events, IAPIHelper aPIHelper, bool hideOrUnhide)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            var tem = OrderTableSqlite.getInvoiceNo(4);
            _invoiceNO = tem == -1 ? 1 : tem + 1;
            AboveInVoiceNo.Text = _invoiceNO.ToString();
            InvoiceDatePicker.SelectedDate = DateTime.Now.Date;
            _hideOrUnhide = hideOrUnhide;
            if (hideOrUnhide)
            {
                RemainingBalanceDockPanel.Visibility = Visibility.Hidden;
                BalanceBueDockPanel.Visibility = Visibility.Hidden;

            }
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
                    _ = _events.PublishOnUIThreadAsync(new RemovePaymentOutEventModel());
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }
        private customerModel _customerData;
        private void CustomerSuggestionTextBox_CustomerSeleted(object sender, CustomerDataEventArgs args)
        {
            _customerData = (customerModel)args.SelectedcustomerData.Clone();
        }

        private PlaceOrderPostRequest placeOrder;
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<int> _prouctNo = new List<int>();
            List<int> _orderQuantitys = new List<int>();
            List<float> _orderSellingPrices = new List<float>();


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
            if (_hideOrUnhide)
            {
                placeOrder.payment.remainingBalance = float.Parse("0");
                placeOrder.payment.TotalBalance = tempReceivebal;
            }

            //order
            placeOrder.order.orderDate = temporderdate;
            placeOrder.order.paymentDone = 1;
            placeOrder.order.customerID = _customerData.customerID;
            placeOrder.order.orderType = 4;
            placeOrder.order.InvoiceNo = int.Parse(AboveInVoiceNo.Text);
            //relation
            placeOrder.productNos = _prouctNo;
            placeOrder.orderQuantitys = _orderQuantitys;
            placeOrder.orderSellingPrices = _orderSellingPrices;


            //call request
            var temp = appConfigSqlite.getData();
            string token = temp.adminToken;
            var result = await _apiHelper.postPlaceOrder(token, placeOrder);
            paymentSqlite.addData(result.data.payment);
            OrderTableSqlite.addData(result.data.order);


            //editing customer amount locally
            //ustomerModel customer = CustomerSqllite.getSingleDataByID(_customerData.customerID);
            customerModel customer = (customerModel)_customerData.Clone();
            customer.TotalAmount += placeOrder.payment.paymentAmount;
            CustomerSqllite.UpdateByID(customer);


            _ = _events.PublishOnUIThreadAsync(new RemovePaymentOutEventModel());
        }
    }
}
