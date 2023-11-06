using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class ItemUnitSqllite
    {
        
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS ItemUnit (unitId INTEGER PRIMARY KEY , " +
                " fullName TEXT , shortName TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(ItemUnitModel itemUnit)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO ItemUnit (unitId  , fullName ,shortName) VALUES (" +
                itemUnit.UnitId.ToString() + ",\'" 
                + itemUnit.FullName + "\',\'"
                + itemUnit.ShortName + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<ItemUnitModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from ItemUnit";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<ItemUnitModel> itemUnits = new List<ItemUnitModel>();

            while (reader.Read())
            {
                var data = new ItemUnitModel(
                    reader.GetInt32(0),
                    (string)reader["fullName"],
                    (string)reader["shortName"]
                    );
                itemUnits.Add(data);
            }
            conn.Close();
            return itemUnits;
        }

        
    }
}
