using DataBaseManger.Model;
using TAN.PostRequest;

namespace TAN.EventModels
{
    public class ShowSalePageEventModel
    {
        public int orderType;
        public int whichMode;
        public OrderTableModel SelectedData;

        public ShowSalePageEventModel(int orderType, int whichMode, OrderTableModel placeOrderPostRequest)
        {
            this.orderType = orderType;
            this.whichMode = whichMode;
            this.SelectedData = placeOrderPostRequest;
        }
    }
}
