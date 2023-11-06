using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class Rl_expense_cat_itemSqllite
    {
        
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS Rl_expense_cat_item (id INTEGER PRIMARY KEY , " +
                " expenseCategaryID INTEGER, expenseItemID INTEGER, date TEXT,  price REAL )";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(Rl_expense_cat_itemModel rl_Expense_Cat_Item)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO expenseCategary (id  , expenseCategaryID,expenseItemID , date ,price ) VALUES (" +
                rl_Expense_Cat_Item.Id.ToString() + ","+
                rl_Expense_Cat_Item.ExpenseCategaryID.ToString() + ","+
                rl_Expense_Cat_Item.ExpenseItemID.ToString() + ",\'" +
                rl_Expense_Cat_Item.Date + "\',"
                + rl_Expense_Cat_Item.Price.ToString() + ")";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Rl_expense_cat_itemModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from expenseCategary";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Rl_expense_cat_itemModel> rl_Expense_Cat_Items = new List<Rl_expense_cat_itemModel>();

            while (reader.Read())
            {
                var data = new Rl_expense_cat_itemModel(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    (string)reader["date"],
                    reader.GetDouble(4)
                    );
                rl_Expense_Cat_Items.Add(data);
            }
            conn.Close();
            return rl_Expense_Cat_Items;
        }

        
    }
}
