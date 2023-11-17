using DataBaseManger.Model;
using System.Data.SQLite;


namespace DataBaseManger.SqlLite
{
    public class AdminTableSqlite
    {
        public AdminTableSqlite() { }

        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS adminTable (" +
                "adminID INTEGER PRIMARY KEY , " +
                "adminEmail TEXT , " +
                "adminPassword TEXT , " +
                "adminToken TEXT ," +
                "isAdmin INTEGER " +
                ")";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(adminModel data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO adminTable (  " +
                "adminID  , " +
                "adminEmail  , " +
                "adminPassword  , " +
                "adminToken  , " +
                "isAdmin ) " +
                "VALUES (" +
                data.adminID.ToString() + ",\'" +
                data.adminEmail + "\',\'" +
                data.adminPassword + "\',\'" +
                data.adminToken + "\', " +
                data.isAdmin + ")";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void editData(adminModel data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "UPDATE adminTable SET  " +
                "adminID  = " + data.adminID.ToString() + " , " +
                "adminEmail = \"" + data.adminEmail + "\" , " +
                "adminPassword = \"" + data.adminPassword + "\" , " +
                "adminToken = \"" + data.adminToken + "\" , " +
                "isAdmin = " + data.isAdmin
                + " WHERE adminID = " + data.adminID.ToString();
                
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        

        public static adminModel getData()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from adminTable where adminID=1";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            adminModel admin = null;

            while (reader.Read())
            {
                admin = new adminModel(
                    reader.GetInt32(0),
                    (string)reader["adminEmail"],
                    (string)reader["adminPassword"],
                    (string)reader["adminToken"],
                    reader.GetInt32(4) );

            }
            conn.Close();
            return admin;
        }
        
    }
}
