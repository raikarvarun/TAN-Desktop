using DataBaseManger.Model;
using System.Collections.Generic;

namespace TAN.PostReqResponse
{
    public class PlaceOrderPostResponse
    {
        public int status { get; set; }
        public string msg { get; set; }
        public string apiVersion { get; set; }
        public temp data { get; set; }
    }
    public class temp
    {
        public paymentModel payment { get; set; }
        public OrderTableModel order { get; set; }

        public temporderProductRelationModel orderproductrelation { get; set; }

    }
    public class temporderProductRelationModel
    {
        public int id { get; set; }

        public int orderID { get; set; }

        public List<int> productNo { get; set; }
        public List<int> orderQuantity { get; set; }
        public List<float> orderSellingPrice { get; set; }

    }
}
