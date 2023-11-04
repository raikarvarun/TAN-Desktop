using DataBaseManger.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DataBaseManger.SqlLite
{
    public class CustomerSqllite
    {
        public CustomerSqllite()
        {

        }

        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS CUSTOMER (" +
                "customerID INTEGER PRIMARY KEY , " +
                "customerName TEXT , " +
                "customerTypeID INTEGER , " +
                "customerMobile TEXT, " +
                "customerAddress TEXT," +
                "customerOpeningAmount REAL," +
                "TotalAmount REAL)";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(customerModel customer)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO CUSTOMER (customerID  , " +
                "customerName  , " +
                "customerTypeID  , " +
                "customerMobile , " +
                "customerAddress ," +
                "customerOpeningAmount ," +
                "TotalAmount) " +
                "VALUES (" +
                customer.customerID.ToString() + ",\'" +
                customer.customerName.ToString() + "\',\'" +
                customer.customerTypeID + "\',\'" +
                customer.customerMobile + "\',\'" +
                customer.customerAddress + "\',\'" +
                customer.customerOpeningAmount + "\',\'" +
                customer.TotalAmount + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<customerModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from CUSTOMER";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<customerModel> customerModels = new List<customerModel>();

            while (reader.Read())
            {

                var customer = new customerModel(
                    reader.GetInt32(0),
                    (string)reader["customerName"],
                    reader.GetInt32(2),
                    (string)reader["customerMobile"],
                    (string)reader["customerAddress"],
                    reader.GetFloat(5),
                    reader.GetFloat(6)
                    );

                customerModels.Add(customer);
            }
            conn.Close();
            return customerModels;
        }
        public static List<string> readOnlyName()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select customerName from CUSTOMER";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<string> customerModels = new List<string>();

            while (reader.Read())
            {


                var customer =
                    (string)reader["customerName"];



                customerModels.Add(customer);
            }
            conn.Close();
            return customerModels;
        }
        public static List<customerModel> searchCustomers(string data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from CUSTOMER where customerName LIKE '%" + data + "%'";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<customerModel> customerModels = new List<customerModel>();

            while (reader.Read())
            {

                var customer = new customerModel(
                    reader.GetInt32(0),
                    (string)reader["customerName"],
                    reader.GetInt32(2),
                    (string)reader["customerMobile"],
                    (string)reader["customerAddress"],
                    reader.GetFloat(5),
                     reader.GetFloat(6)
                    );

                customerModels.Add(customer);
            }
            conn.Close();
            return customerModels;
        }

        public static customerModel getSingleDataByID(int customerID)
        {

            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from CUSTOMER where customerID = " + customerID.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            customerModel customer = null;

            while (reader.Read())
            {

                customer = new customerModel(
                    reader.GetInt32(0),
                    (string)reader["customerName"],
                    reader.GetInt32(2),
                    (string)reader["customerMobile"],
                    (string)reader["customerAddress"],
                     reader.GetFloat(5),
                     reader.GetFloat(6)
                    );

            }
            conn.Close();
            return customer;
        }

        public static void UpdateByID(customerModel customer)
        {

            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "UPDATE CUSTOMER SET " +
                " customerName = \"" + customer.customerName + "\"," +
                " customerTypeID = " + customer.customerTypeID.ToString() + "," +
                " customerMobile =  \"" + customer.customerMobile + "\"," +
                " customerAddress = \"" + customer.customerAddress + "\"," +
                " customerOpeningAmount = " + customer.customerOpeningAmount.ToString() + "," +
                " TotalAmount = " + customer.TotalAmount.ToString() +
                " where customerID = " + customer.customerID.ToString();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            conn.Close();
        }

        public static List<CustomerTransationmodel> readTransationbyId(int customerID)
        {
            List<CustomerTransationmodel> customerTransationmodels = new List<CustomerTransationmodel>();

            List<OrderTableModel> orderTable = OrderTableSqlite.getDatabyCustomerID(customerID);

            foreach (OrderTableModel model in orderTable)
            {
                paymentModel payment = paymentSqlite.getSingleDataByID(model.paymentID);

                string type1 = OrderTableSqlite.getOrderTypebyId(model.orderType);
                string ordredate = Convert.ToDateTime(model.orderDate).ToString("dd-MM-yyyy");


                SolidColorBrush CircleColor = ProductVersionModelSqlite.findCircleColourByTypeID(model.orderType);
                var myPath = new Path();
                myPath.Fill = CircleColor;
                myPath.Stretch = Stretch.Uniform;
                myPath.Width = 8;
                myPath.Height = 8;
                myPath.Data = Geometry.Parse("M480 976q-82 0-155-31.5t-127.5-86Q143 804 111.5 731T80 576q0-83 31.5-156t86-127Q252 239 325 207.5T480 176q83 0 156 31.5T763 293q54 54 85.5 127T880 576q0 82-31.5 155T763 858.5q-54 54.5-127 86T480 976Z");

                string totalBalance = "₹ " + CommanSQlite.DisplayIndianCurrency(payment.TotalBalance.ToString());
                string remBalance = "₹ " +  CommanSQlite.DisplayIndianCurrency(payment.remainingBalance.ToString());
                CustomerTransationmodel customerTransation = new CustomerTransationmodel(myPath, 
                    type1, model.InvoiceNo.ToString(), ordredate, totalBalance, remBalance
                    );
                

                customerTransationmodels.Add(customerTransation);
            }
            return customerTransationmodels;
        }
    }
}
