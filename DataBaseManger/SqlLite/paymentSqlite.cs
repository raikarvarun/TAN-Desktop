using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class paymentSqlite
    {
        public paymentSqlite()
        {

        }
        public static void deleteByID(string orderID)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "DELETE FROM PAYMENT WHERE paymentID = " + orderID;

            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS PAYMENT (paymentID INTEGER PRIMARY KEY , " +
                "paymentType INTEGER , paymentDate TEXT , paymentAmount REAL , remainingBalance REAL , TotalBalance REAL)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(paymentModel payment)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO PAYMENT (paymentID  , paymentType  , " +
                "paymentDate  , paymentAmount ,remainingBalance ,TotalBalance) VALUES (" +
                payment.paymentID.ToString() + ",\'" + payment.paymentType.ToString() + "\',\'"
                + payment.paymentDate + "\',\'"
                + payment.paymentAmount.ToString() + "\',\'"
                + payment.remainingBalance.ToString() + "\',\'"
                + payment.TotalBalance.ToString() + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<paymentModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from PAYMENT";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<paymentModel> paymentModels = new List<paymentModel>();

            while (reader.Read())
            {
                var payment = new paymentModel(reader.GetInt32(0),
                    reader.GetInt32(1), (string)reader["paymentDate"],
                    reader.GetFloat(3), reader.GetFloat(4), reader.GetFloat(5));
                paymentModels.Add(payment);
            }
            conn.Close();
            return paymentModels;
        }

        public static paymentModel getSingleDataByID(int paymentID)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from PAYMENT where paymentID = " + paymentID;
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            paymentModel payment = null;

            while (reader.Read())
            {
                payment = new paymentModel(reader.GetInt32(0), reader.GetInt32(1),
                    (string)reader["paymentDate"], reader.GetFloat(3), reader.GetFloat(4), reader.GetFloat(5));
                break;
            }
            conn.Close();
            return payment;
        }

        public static List<paymentModel> getDataByType(int data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from PAYMENT where paymentType = " + data.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<paymentModel> paymentModels = new List<paymentModel>();

            while (reader.Read())
            {
                var payment = new paymentModel(reader.GetInt32(0),
                    reader.GetInt32(1), (string)reader["paymentDate"],
                    reader.GetFloat(3), reader.GetFloat(4), reader.GetFloat(5));
                paymentModels.Add(payment);
            }
            conn.Close();
            return paymentModels;
        }
    }
}
