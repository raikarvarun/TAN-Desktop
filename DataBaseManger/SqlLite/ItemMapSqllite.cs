using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class ItemMapSqllite
    {
        
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS ItemMap (mapID INTEGER PRIMARY KEY , " +
                " baseUnitID INTEGER , secondUnitID INTEGER , rate REAL  )";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(ItemMapModel itemMap)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO ItemMap (mapID  , baseUnitID , secondUnitID , rate ) VALUES (" +
                itemMap.MapID.ToString() + "," 
                + itemMap.BaseUnitID.ToString() + ","
                + itemMap.SecondUnitID.ToString() + ","
                + itemMap.Rate.ToString() + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<ItemMapModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from ItemMap";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<ItemMapModel> itemMaps = new List<ItemMapModel>();

            while (reader.Read())
            {
                var data = new ItemMapModel(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDouble(3)
                    
                    );
                itemMaps.Add(data);
            }
            conn.Close();
            return itemMaps;
        }

        
    }
}
