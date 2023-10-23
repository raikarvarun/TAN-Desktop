

namespace DataBaseManger.Model
{
    public class PaymentInModel
    {
        public int SerialNo { get; set; }
        public string Date { get; set; }
        public int InvoiceNo { get; set; }

        public string PartyName { get; set; }

        public string OrderType { get; set; }
        public float TotalAmount { get; set; }
        public float Amount { get; set; }
        public float BalanceDue { get; set; }

        public PaymentInModel(int serialNO, string date, int invoiceNo, string partyname,
            string orderType,
            float totalamount,

            float amount,
            float balance)
        {
            SerialNo = serialNO;
            Date = date;
            InvoiceNo = invoiceNo;
            PartyName = partyname;
            OrderType = orderType;
            TotalAmount = totalamount;
            Amount = amount;
            BalanceDue = balance;
        }
    }
}
