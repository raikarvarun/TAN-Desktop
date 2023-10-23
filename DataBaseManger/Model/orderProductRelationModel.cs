namespace DataBaseManger.Model
{
    public class orderProductRelationModel
    {
        public int orderID { get; set; }
        public int productNo { get; set; }
        public int orderQuantity { get; set; }
        public float orderSellingPrice { get; set; }
        //Order selling price is order price for all



        public orderProductRelationModel(int aorderID,
            int aproductNo,
            int aorderQuantity,
            float aorderSellingPrice)
        {
            orderID = aorderID;
            productNo = aproductNo;
            orderQuantity = aorderQuantity;
            orderSellingPrice = aorderSellingPrice;

        }
    }
}
