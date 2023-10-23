namespace DataBaseManger.Model
{
    public class customerModel
    {
        public int customerID { get; set; }
        public string customerName { get; set; }
        public int customerTypeID { get; set; }
        public string customerMobile { get; set; }
        public string customerAddress { get; set; }

        public float customerOpeningAmount { get; set; }
        public float TotalAmount { get; set; }



        public customerModel(int CustomerID, string CustomerName, int CustomerTypeID,
            string CustomerMobile, string CustomerAddress, float CustomerOpeningAmount, float totalAmount)
        {
            customerID = CustomerID;
            customerName = CustomerName;
            customerTypeID = CustomerTypeID;
            customerMobile = CustomerMobile;
            customerAddress = CustomerAddress;
            customerOpeningAmount = CustomerOpeningAmount;
            TotalAmount = totalAmount;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
