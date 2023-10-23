namespace DataBaseManger.Model
{
    public class OrderTableModel
    {
        public int orderID { get; set; }
        public string orderDate { get; set; }
        public int paymentDone { get; set; }
        public int customerID { get; set; }
        public int paymentID { get; set; }

        public int orderType { get; set; }
        public int InvoiceNo { get; set; }

        public OrderTableModel(int OrderID, string OrderDate, int PaymentDone,
            int CustomerID, int PaymentID, int aorderType, int aInvoiceNo)
        {
            orderID = OrderID;
            orderDate = OrderDate;
            paymentDone = PaymentDone;
            customerID = CustomerID;
            paymentID = PaymentID;
            orderType = aorderType;
            InvoiceNo = aInvoiceNo;
        }
    }
}
