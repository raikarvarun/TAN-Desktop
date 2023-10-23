namespace DataBaseManger.Model
{
    public class PaymentTypeModel
    {
        public int paymentTypeId { get; set; }
        public string paymentTypeType { get; set; }
        public string paymentTypeName { get; set; }

        public PaymentTypeModel(int PaymentTypeId, string PaymentTypeType, string PaymentTypeName)
        {
            paymentTypeId = PaymentTypeId;
            paymentTypeType = PaymentTypeType;
            paymentTypeName = PaymentTypeName;
        }
    }
}
