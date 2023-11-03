using System.Runtime.CompilerServices;

namespace TAN.EventModels
{

    public class PaymentInEventModel
    {
        public int orderType;
        public PaymentInEventModel(int orderType)
        {
            this.orderType = orderType;
        }
    }
}
