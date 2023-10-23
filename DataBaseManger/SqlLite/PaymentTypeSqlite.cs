using DataBaseManger.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataBaseManger.SqlLite
{
    public class PaymentTypeSqlite
    {
        public static void createTable()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "CREATE TABLE IF NOT EXISTS paymentType (paymentTypeId INTEGER PRIMARY KEY , " +
                "paymentTypeType TEXT , paymentTypeName TEXT )";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void addData(PaymentTypeModel paymentType)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "INSERT INTO paymentType (paymentTypeId  , paymentTypeType  , " +
                "paymentTypeName  ) VALUES (" +
                paymentType.paymentTypeId.ToString() + ",\'"
                + paymentType.paymentTypeType.ToString() + "\',\'"
                + paymentType.paymentTypeName + "\')";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<PaymentTypeModel> readAll()
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from paymentType";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            List<PaymentTypeModel> paymentModels = new List<PaymentTypeModel>();

            while (reader.Read())
            {
                var payment = new PaymentTypeModel(reader.GetInt32(0),
                     (string)reader["paymentTypeType"], (string)reader["paymentTypeName"]);
                paymentModels.Add(payment);
            }
            conn.Close();
            return paymentModels;
        }

        public static List<BankTransactoin> getDatabyIDforBankTransaction(int data)
        {
            List<BankTransactoin> bankTransactoins = new List<BankTransactoin>();
            List<paymentModel> paymentModels = paymentSqlite.getDataByType(data);
            foreach (paymentModel payment in paymentModels)
            {
                OrderTableModel order = OrderTableSqlite.getDatabyPaymentID(payment.paymentID);
                customerModel customer = CustomerSqllite.getSingleDataByID(order.customerID);
                string type1 = OrderTableSqlite.getOrderTypebyId(order.orderType);
                string ordredate = Convert.ToDateTime(order.orderDate).ToString("dd-MM-yyyy");
                BankTransactoin bankTransactoin = new BankTransactoin();
                bankTransactoin.Type = type1;
                bankTransactoin.Name = customer.customerName;
                bankTransactoin.Date = ordredate;
                bankTransactoin.Amount = payment.TotalBalance.ToString();
                bankTransactoins.Add(bankTransactoin);

            }

            return bankTransactoins;
        }
    }
}
