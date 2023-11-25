using DataBaseManger.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DataBaseManger.SqlLite
{
    public class ProductVersionModelSqlite
    {
        public ProductVersionModelSqlite() { }
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS PRODUCTVERSION (productNo INTEGER PRIMARY KEY , " +
                " productType TEXT , " +
                "productPrice REAL , " +
                "productQuntity INTEGER , " +
                "productImage TEXT , " +
                "productName TEXT , " +
                "openingQuantity INTEGER , " +
                "atprice REAL , " +
                "salePrice REAL , " +
                "purchasePrice REAL)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(productVersionModel productVersion)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO PRODUCTVERSION " +
                "(productNo   , productType  , productPrice , " +
                "productQuntity  ,productImage" +
                " ,productName , openingQuantity ,atprice" +
                ",salePrice,purchasePrice) " +
                "VALUES ("
                + productVersion.productNo.ToString() + ",\'"
                + productVersion.productType + "\',\'"
                + productVersion.productPrice + "\',\'"
                + productVersion.productQuntity + "\',\'"
                + productVersion.productImage + "\',\'"
                + productVersion.productName + "\',\'"
                + productVersion.openingQuantity + "\',\'"
                + productVersion.atprice + "\',\'"
                + productVersion.salePrice + "\',\'"
                + productVersion.purchasePrice + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<productVersionModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from PRODUCTVERSION";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<productVersionModel> productVersionModels = new List<productVersionModel>();

            while (reader.Read())
            {
                var productVersion = new productVersionModel(
                    reader.GetInt32(0),
                    (string)reader["productType"],
                    reader.GetFloat(2),
                    reader.GetInt32(3),
                    (string)reader["productImage"],
                    (string)reader["productName"],
                    reader.GetInt32(6),
                    reader.GetFloat(7),
                    reader.GetFloat(8),
                    reader.GetFloat(9)
                    );
                productVersionModels.Add(productVersion);
            }
            conn.Close();
            return productVersionModels;
        }


        public static List<productVersionModel> searchProduct(string data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from PRODUCTVERSION where productName LIKE '%" + data + "%'";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<productVersionModel> productVersionModels = new List<productVersionModel>();

            while (reader.Read())
            {
                var productVersion = new productVersionModel(
                    reader.GetInt32(0),
                    (string)reader["productType"],
                    reader.GetFloat(2),
                    reader.GetInt32(3),
                    (string)reader["productImage"],
                    (string)reader["productName"],
                    reader.GetInt32(6),
                    reader.GetFloat(7),
                    reader.GetFloat(8),
                    reader.GetFloat(9)
                    );
                productVersionModels.Add(productVersion);
            }
            conn.Close();
            return productVersionModels;
        }


        public static productVersionModel getSingleDataByID(int data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from PRODUCTVERSION where productNo = " + data.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            productVersionModel productVersion = null;
            while (reader.Read())
            {
                productVersion = new productVersionModel(
                    reader.GetInt32(0),
                    (string)reader["productType"],
                    reader.GetFloat(2),
                    reader.GetInt32(3),
                    (string)reader["productImage"],
                    (string)reader["productName"],
                    reader.GetInt32(6),
                    reader.GetFloat(7),
                    reader.GetFloat(8),
                    reader.GetFloat(9)
                    );
            }
            conn.Close();
            return productVersion;
        }

        public static void UpdateProductByID(productVersionModel productVersionModel)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "Update PRODUCTVERSION Set" +
                " productType = \'" + productVersionModel.productType + "\'" +
                ", productPrice = " + productVersionModel.productPrice.ToString() +
                ", productQuntity = " + productVersionModel.productQuntity +
                ", productImage = \'" + productVersionModel.productImage + "\'" +
                ", productName = \'" + productVersionModel.productName + "\'" +
                ", openingQuantity = " + productVersionModel.openingQuantity +
                ", atprice = " + productVersionModel.atprice +
                ", salePrice = " + productVersionModel.salePrice +
                ", purchasePrice = " + productVersionModel.purchasePrice +


                " where productNo = " + productVersionModel.productNo.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();

        }

        public static List<ItemTransactionModel> readAllTransaction(int serchKey)
        {
            List<ItemTransactionModel> itemTransactions = new List<ItemTransactionModel>();
            List<orderProductRelationModel> orderProductRelations = orderProductRelationModelSqlite.readAllbyProductNO(serchKey);
            List<OrderTableModel> orderTableModels = new List<OrderTableModel>();
            foreach (orderProductRelationModel order in orderProductRelations)
            {
                OrderTableModel order1 = OrderTableSqlite.getSingleDatabyId(order.orderID);
                orderTableModels.Add(order1);
                customerModel customer = CustomerSqllite.getSingleDataByID(order1.customerID);
                paymentModel payment = paymentSqlite.getSingleDataByID(order1.paymentID);

                string type = OrderTableSqlite.getOrderTypebyId(order1.orderType);
                string status = "";
                if (payment.remainingBalance == 0)
                {
                    status = "Paid";
                }
                else
                {
                    status = "Unpaid";
                }
                string price = "₹ " + order.orderSellingPrice.ToString();
                string ordredate = Convert.ToDateTime(order1.orderDate).ToString("dd-MM-yyyy");

                SolidColorBrush CircleColor = findCircleColourByTypeID(order1.orderType);
                var myPath = new Path();
                myPath.Fill = CircleColor;
                myPath.Stretch = Stretch.Uniform; 
                myPath.Width = 8;
                myPath.Height = 8;
                myPath.Data = Geometry.Parse("M480 976q-82 0-155-31.5t-127.5-86Q143 804 111.5 731T80 576q0-83 31.5-156t86-127Q252 239 325 207.5T480 176q83 0 156 31.5T763 293q54 54 85.5 127T880 576q0 82-31.5 155T763 858.5q-54 54.5-127 86T480 976Z");



                ItemTransactionModel item = new ItemTransactionModel(myPath, type, customer.customerName, ordredate, order.orderQuantity.ToString()
                    , price, status);
                itemTransactions.Add(item);
            }
            return itemTransactions;
        }
        public static SolidColorBrush findCircleColourByTypeID(int typeID)
        {
            SolidColorBrush result = null;
            switch (typeID)
            {
                case 1:
                    //ans = "Sale";
                    result = new SolidColorBrush(Color.FromArgb(255, 144, 228, 146));
                    break;
                case 2:
                    //ans = "Purchase";
                    result = new SolidColorBrush(Color.FromArgb(255, 214, 108, 108));
                    break;
                case 3:
                    //ans = "PaymentIn";
                    result = new SolidColorBrush(Color.FromArgb(255, 89, 168, 111));
                    break;
                case 4:
                    //ans = "PaymentOut";
                    result = new SolidColorBrush(Color.FromArgb(255, 222, 160, 75));
                    break;
                case 5:
                    //ans = "Credit Note";
                    result = new SolidColorBrush(Color.FromArgb(255, 235, 166, 125));
                    break;
                case 6:
                    //ans = "Debit Note";
                    result = new SolidColorBrush(Color.FromArgb(255, 199, 126, 184));
                    break;
                case 8:
                    //ans = "Subscription ";
                    result = new SolidColorBrush(Color.FromArgb(255, 89, 168, 111));
                    break;
                case 9:
                    //ans = "Subscription sale";
                    result = new SolidColorBrush(Color.FromArgb(255, 144, 228, 146));
                    break;
                case 10:
                    //ans = "Subscription cancel";
                    result = new SolidColorBrush(Color.FromArgb(255, 214, 108, 108));
                    break;
            }

            return result;
        }
    }
}
