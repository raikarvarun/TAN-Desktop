using DataBaseManger.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class orderProductRelationModelSqlite
    {
        public static void deleteByID(string orderID)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "DELETE FROM orderProductRelationModel WHERE orderID = " + orderID;

            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS orderProductRelationModel " +
                "(orderID INTEGER , " +
                "productNo INTEGER ,  " +
                "orderQuantity INTEGER , " +
                "orderSellingPrice REAL )";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(orderProductRelationModel orderProductRelation)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO orderProductRelationModel " +
                "(orderID  , productNo   , orderQuantity , orderSellingPrice) " +
                "VALUES ("
                + orderProductRelation.orderID.ToString() + ",\'"
                + orderProductRelation.productNo.ToString() + "\',\'"
                + orderProductRelation.orderQuantity + "\',\'"
                + orderProductRelation.orderSellingPrice + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<orderProductRelationModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from orderProductRelationModel";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<orderProductRelationModel> orderProductRelationModels = new List<orderProductRelationModel>();

            while (reader.Read())
            {
                var orderProductRelation = new orderProductRelationModel(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    (int)reader["orderQuantity"],
                    (float)reader["orderSellingPrice"]
                    );
                orderProductRelationModels.Add(orderProductRelation);
            }
            conn.Close();
            return orderProductRelationModels;
        }

        public static List<orderProductRelationModel> readAllbyProductNO(int searchKey)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from orderProductRelationModel where productNo = " + searchKey.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<orderProductRelationModel> orderProductRelationModels = new List<orderProductRelationModel>();

            while (reader.Read())
            {
                var orderProductRelation = new orderProductRelationModel(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetFloat(3)
                    );
                orderProductRelationModels.Add(orderProductRelation);
            }
            conn.Close();
            return orderProductRelationModels;
        }
        public static List<orderProductRelationModel> readAllbyOrderID(int searchKey)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from orderProductRelationModel where orderID = " + searchKey.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<orderProductRelationModel> orderProductRelationModels = new List<orderProductRelationModel>();

            while (reader.Read())
            {
                var orderProductRelation = new orderProductRelationModel(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetFloat(3)
                    );
                orderProductRelationModels.Add(orderProductRelation);
            }
            conn.Close();
            return orderProductRelationModels;
        }
    }
}
