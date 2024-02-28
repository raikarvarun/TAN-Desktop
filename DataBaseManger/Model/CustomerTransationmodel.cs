using System.Diagnostics;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DataBaseManger.Model
{
    public class CustomerTransationmodel
    {
        public Path urlimg { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }

        public int orderID { get; set; }
        public int paymentID { get; set; }

        public string Date1 { get; set; }
        public string Total { get; set; }

        public string Balance { get; set; }
        public CustomerTransationmodel(Path UrlImg, string type, string number, string date1,
                string total, string balance, int orderID, int paymentID)
        {
            urlimg = UrlImg;
            Type = type;
            Number = number;
            Date1 = date1;
            Total = total;
            Balance = balance;
            this.orderID = orderID;
            this.paymentID = paymentID;
        }
    }
}
