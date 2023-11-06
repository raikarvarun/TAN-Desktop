using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class ExpenseItemSqllite
    {
        
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS expenseItem (expenseItemID INTEGER PRIMARY KEY , " +
                " expenseItemName TEXT , openingAmount REAL)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(ExpenseItemModel expenseItem)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO expenseItem (expenseItemID  , expenseItemName, openingAmount) VALUES (" +
                expenseItem.ExpenseItemID.ToString() + ",\'" 
                + expenseItem.ExpenseItemName  + "\'," + 
                expenseItem.OpeningAmount.ToString() + ")";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<ExpenseItemModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from expenseItem";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<ExpenseItemModel> expenseItems = new List<ExpenseItemModel>();

            while (reader.Read())
            {
                var data = new ExpenseItemModel(
                    reader.GetInt32(0),
                    (string)reader["expenseCategaryName"],
                    reader.GetDouble(2)
                    );
                expenseItems.Add(data);
            }
            conn.Close();
            return expenseItems;
        }

        
    }
}
