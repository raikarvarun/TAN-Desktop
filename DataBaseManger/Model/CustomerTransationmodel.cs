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

        public string Date1 { get; set; }
        public string Total { get; set; }

        public string Balance { get; set; }
        public CustomerTransationmodel(Path UrlImg, string type, string number, string date1, 
                string total, string balance)
        {
            urlimg = UrlImg;
            Type = type;
            Number = number;
            Date1 = date1;
            Total = total;
            Balance = balance;
            
        }
    }
}
