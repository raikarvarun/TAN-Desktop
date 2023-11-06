using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class ExpenseCategorySqllite
    {
        
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS expenseCategary (expenseCategaryID INTEGER PRIMARY KEY , " +
                " expenseCategaryName TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(ExpenseCategoryModel expenseCategory)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO expenseCategary (expenseCategaryID  , expenseCategaryName) VALUES (" +
                expenseCategory.ExpenseCategaryID.ToString() + ",\'" 
                + expenseCategory.ExpenseCategaryName + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<ExpenseCategoryModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from expenseCategary";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<ExpenseCategoryModel> expenseCategories = new List<ExpenseCategoryModel>();

            while (reader.Read())
            {
                var payment = new ExpenseCategoryModel(
                    reader.GetInt32(0),
                    (string)reader["expenseCategaryName"]
                    );
                expenseCategories.Add(payment);
            }
            conn.Close();
            return expenseCategories;
        }

        
    }
}
