namespace DataBaseManger.Model
{
    public class ChartSalesModel
    {

        
        public string saleDate { get; set; }
        public float saleAmount { get; set; }
        


        public ChartSalesModel(string SaleDate , float SaleAmount)
        {
            saleDate = SaleDate;
            saleAmount = SaleAmount;
        }
    }
}
