namespace DataBaseManger.Model
{
    public class PurchaseHomeViewBoxModel
    {
        public string ProductName { get; set; }
        public float Amount { get; set; }



        public PurchaseHomeViewBoxModel(string productName, float amount)
        {
            ProductName = productName;
            Amount = amount;
            
            
        }
    }
}
