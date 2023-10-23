namespace DataBaseManger.Model
{
    //class is using by salePageView
    public class ItemdataModel
    {
        public int ProductNo;
        public int ItemNO { get; set; }
        public string ItemName { get; set; }

        public int ItemQuntity { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }

        public ItemdataModel(int productNo, int itemNo, string itemName, int itemQuntity, float price, float amount)
        {
            ProductNo = productNo;
            ItemNO = itemNo;
            ItemName = itemName;
            ItemQuntity = itemQuntity;
            Price = price;
            Amount = amount;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
