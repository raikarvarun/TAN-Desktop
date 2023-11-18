using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DataBaseManger.SqlLite
{
    public class appConfigSqlite
    {
        public appConfigSqlite() { }

        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS appconfig (" +
                "appconfigID INTEGER PRIMARY KEY , " +
                "appconfigName TEXT , " +
                "appconfigVersion TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(appConfigModel appConfig)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO appconfig (  " +
                "appconfigID  , " +
                "appconfigName  , " +
                "appconfigVersion ) " +
                "VALUES (" +
                appConfig.appconfigID.ToString() + ",\'" +
                appConfig.appconfigName + "\',\'" +
                appConfig.appconfigVersion + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void editData(string name , string version)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "UPDATE appconfig SET  " +
                "appconfigVersion = \"" + version
                + "\"  "
                + " WHERE appconfigName = \"" + name +"\"";
                
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        

        public static List<appConfigModel> getData()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from appconfig";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            List<appConfigModel> ans = new List<appConfigModel>();

            appConfigModel appConfig = null;

            while (reader.Read())
            {
                appConfig = new appConfigModel(
                    reader.GetInt32(0),
                    (string)reader["appconfigName"],
                    (string)reader["appconfigVersion"]
                    );
                ans.Add(appConfig);

            }
            conn.Close();
            return ans;
        }

        
    }
}
