namespace DataBaseManger.Model
{
    public class productVersionModel
    {
        public int productNo { get; set; }
        public string productType { get; set; }
        public float productPrice { get; set; }
        public int productQuntity { get; set; }
        public string productImage { get; set; }
        public string productName { get; set; }
        public int openingQuantity { get; set; }
        public float atprice { get; set; }
        public float salePrice { get; set; }
        public float purchasePrice { get; set; }


        public productVersionModel(
            int aproductNo,
            string aproductType,
            float aproductPrice,
            int aproductQuntity,
            string aproductImage,
            string aproductName,
            int aopeningQuantity,
            float Atprice,
            float SalePrice,
            float PurchasePrice
            )
        {
            productNo = aproductNo;
            productType = aproductType;
            productPrice = aproductPrice;
            productQuntity = aproductQuntity;
            productImage = aproductImage;
            productName = aproductName;
            openingQuantity = aopeningQuantity;
            atprice = Atprice;
            salePrice = SalePrice;
            purchasePrice = PurchasePrice;
        }
    }
}
