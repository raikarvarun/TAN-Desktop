namespace DataBaseManger.Model
{
    public class ExpenseItemModel
    {
        public int ExpenseItemID { get; set; }
        public string ExpenseItemName { get; set; }
        public double OpeningAmount { get; set; }
        

        public ExpenseItemModel(int ExpenseItemID, string ExpenseItemName, double OpeningAmount)
        {
            this.ExpenseItemID = ExpenseItemID;
            this.ExpenseItemName = ExpenseItemName;
            this.OpeningAmount = OpeningAmount;
            
        }
    }
}
