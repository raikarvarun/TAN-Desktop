namespace DataBaseManger.Model
{
    public class Rl_expense_cat_itemModel
    {
        public int Id { get; set; }
        public int ExpenseCategaryID { get; set; }
        public int ExpenseItemID { get; set; }
        public string Date { get; set; }
        public double Price { get; set; }

        

        public Rl_expense_cat_itemModel(int Id, int ExpenseCategaryID, int ExpenseItemID,
            string Date, double Price)
        {
            this.Id = Id;
            this.ExpenseCategaryID = ExpenseCategaryID;
            this.ExpenseItemID = ExpenseItemID;
            this.Date = Date;
            this.Price = Price;
            
        }
    }
}
