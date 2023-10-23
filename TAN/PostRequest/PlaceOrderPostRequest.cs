using System.Collections.Generic;

namespace TAN.PostRequest
{
    public class PlaceOrderPostRequest
    {
        public temp1 payment = new temp1();
        public temp2 order = new temp2();
        public List<int> productNos { get; set; }
        public List<int> orderQuantitys { get; set; }
        public List<float> orderSellingPrices { get; set; }
    }
    public class temp1
    {
        public int paymentType { get; set; }
        public string paymentDate { get; set; }
        public float paymentAmount { get; set; }
        public float remainingBalance { get; set; }

        public float TotalBalance { get; set; }
    }
    public class temp2
    {
        public string orderDate { get; set; }
        public int paymentDone { get; set; }
        public int customerID { get; set; }
        public int orderType { get; set; }
        public int InvoiceNo { get; set; }
    }
}
