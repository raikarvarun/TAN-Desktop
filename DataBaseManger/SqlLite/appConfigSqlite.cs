using DataBaseManger.Model;
using System.Data.SQLite;

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
                "appID INTEGER PRIMARY KEY , " +
                "adminEmail TEXT , " +
                "adminPassword TEXT , " +
                "adminToken TEXT, " +
                "apiVersion TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(appConfigModel appConfig)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO appconfig (  " +
                "appID  , " +
                "adminEmail  , " +
                "adminPassword , " +
                "adminToken ," +
                "apiVersion) " +
                "VALUES (" +
                appConfig.appID.ToString() + ",\'" +
                appConfig.adminEmail + "\',\'" +
                appConfig.adminPassword + "\',\'" +
                appConfig.adminToken + "\',\'" +
                appConfig.apiVersion + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static appConfigModel getData()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from appconfig where appID=1";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            appConfigModel appConfig = null;

            while (reader.Read())
            {
                appConfig = new appConfigModel(
                    reader.GetInt32(0),
                    (string)reader["adminEmail"],
                    (string)reader["adminPassword"],
                    (string)reader["adminToken"],
                    (string)reader["apiVersion"]);

            }
            conn.Close();
            return appConfig;
        }
        public static void deleteData()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "DELETE FROM appconfig WHERE appID=1";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
