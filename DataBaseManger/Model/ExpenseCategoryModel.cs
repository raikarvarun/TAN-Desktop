namespace DataBaseManger.Model
{
    public class ExpenseCategoryModel
    {
        public int ExpenseCategaryID { get; set; }
        public string ExpenseCategaryName { get; set; }
        

        public ExpenseCategoryModel(int ExpenseCategaryID, string ExpenseCategaryName)
        {
            this.ExpenseCategaryID = ExpenseCategaryID;
            this.ExpenseCategaryName = ExpenseCategaryName;
            
        }
    }
}
