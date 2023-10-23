

using System.Windows.Shapes;

namespace DataBaseManger.Model
{
    public class ItemTransactionModel
    {
        //public Button button { get; set; }
        public Path urlimg { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }

        public string Qty { get; set; }
        public string Price { get; set; }

        public string Status { get; set; }

        public ItemTransactionModel(Path UrlImg, string type, string name, string date, string qty, string price, string status)
        {
            urlimg = UrlImg;
            Type = type;
            Name = name;
            Date = date;
            Qty = qty;
            Price = price;
            Status = status;
        }
    }
}
