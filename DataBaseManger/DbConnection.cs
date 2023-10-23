using System;
using System.Data.SQLite;
using System.IO;

namespace DataBaseManger
{
    public class DbConnection
    {
        public static SQLiteConnection createDbConnection()
        {
            string dBFile = "URI=file:MySqlite.db";
            SQLiteConnection conn = new SQLiteConnection(dBFile);
            return conn;
        }

        public static void deleteDb()
        {
            string DatabasePath = "MySqlite.db";
            FileInfo fi = new FileInfo(DatabasePath);
            try
            {
                if (fi.Exists)
                {
                    SQLiteConnection connection = new SQLiteConnection("Data Source=" + DatabasePath + ";");
                    connection.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                fi.Delete();
                Console.WriteLine(ex.ToString());
            }
        }

        public static bool DBExists()
        {
            string DatabasePath = "MySqlite.db";
            FileInfo fi = new FileInfo(DatabasePath);

            if (fi.Exists)
            {
                return true;
            }
            return false;

        }
    }
}
