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

        public static void editData(appConfigModel appConfig)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "UPDATE appconfig SET  " +
                "appconfigID  = " + appConfig.appconfigID.ToString() + " , " +
                "appconfigName = \"" + appConfig.appconfigName + "\" , " +
                "appconfigVersion = \"" + appConfig.appconfigVersion
                + "\"  "
                + " WHERE appconfigName = " + appConfig.appconfigName.ToString();
                
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
                    (string)reader["appconfigName"],
                    (string)reader["appconfigVersion"]
                    );

            }
            conn.Close();
            return appConfig;
        }
        //public static void deleteData()
        //{
        //    SQLiteConnection conn = DbConnection.createDbConnection();
        //    conn.Open();
        //    string query = "DELETE FROM appconfig WHERE appID=1";
        //    SQLiteCommand command = new SQLiteCommand(query, conn);
        //    command.ExecuteNonQuery();
        //    conn.Close();
        //}
    }
}
