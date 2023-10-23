namespace DataBaseManger.Model
{
    public class SaleInvoiceModel
    {
        public string Date { get; set; }
        public int InvoiceNo { get; set; }

        public string PartyName { get; set; }

        public int PaymentType { get; set; }
        public float Amount { get; set; }
        public float BalanceDue { get; set; }

        public SaleInvoiceModel(string date, int invoiceNo, string partyname, int paymentType, float amount, float balance)
        {
            Date = date;
            InvoiceNo = invoiceNo;
            PartyName = partyname;
            PaymentType = paymentType;
            Amount = amount;
            BalanceDue = balance;
        }

    }
}
