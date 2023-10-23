namespace DataBaseManger.Model
{
    public class paymentModel
    {
        public int paymentID { get; set; }
        public int paymentType { get; set; }
        public string paymentDate { get; set; }
        public float paymentAmount { get; set; }

        public float remainingBalance { get; set; }

        public float TotalBalance { get; set; }


        public paymentModel(int PaymentID, int PaymentType, string PaymentDate, float PaymentAmount
            , float REmaininnBalance, float TotalBalance1)
        {
            paymentID = PaymentID;
            paymentType = PaymentType;
            paymentDate = PaymentDate;
            paymentAmount = PaymentAmount;
            remainingBalance = REmaininnBalance;
            TotalBalance = TotalBalance1;
        }
    }
}
