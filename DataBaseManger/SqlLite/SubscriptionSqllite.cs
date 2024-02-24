using DataBaseManger.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;



namespace DataBaseManger.SqlLite
{
    public class SubscriptionSqllite
    {
        public SubscriptionSqllite()
        {

        }

        //public static PlaceOrderPostRequestDataBase getAllEditableDataforSubsciptionByCustomerID(int customerID)
        //{

        //    var order = getOrderData(customerID);
        //    var payment = paymentSqlite.getSingleDataByID(order.paymentID);
        //    var orderProductRelation = orderProductRelationModelSqlite.readAllbyOrderID(order.orderID);


        //    var ans = new PlaceOrderPostRequestDataBase();

        //    ans.payment.paymentType = payment.paymentType;
        //    ans.payment.paymentDate = payment.paymentDate;
        //    ans.payment.paymentAmount = payment.paymentAmount;
        //    ans.payment.remainingBalance = payment.remainingBalance;
        //    ans.payment.TotalBalance = payment.TotalBalance;

        //    ans.order.orderDate = order.orderDate;
        //    ans.order.paymentDone = order.paymentDone;
        //    ans.order.customerID = order.customerID;
        //    ans.order.orderType = order.orderType;
        //    ans.order.InvoiceNo = order.InvoiceNo;


        //    return ans;

        //}

        public static OrderTableModel getSingleOrderDataByCustomerIDandOrdertype(int data)
        {
            SQLiteConnection conn = DbConnection.createDbConnection();
            conn.Open();
            string query = "select * from OrderTable where customerID = " + data.ToString() + " and orderType = 8 ;";
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            OrderTableModel ordertable = null;

            while (reader.Read())
            {


                ordertable = new OrderTableModel(
                   reader.GetInt32(0),
                   (string)reader["orderDate"],
                   reader.GetInt32(2),
                   reader.GetInt32(3),
                   reader.GetInt32(4),
                   reader.GetInt32(5),
                   reader.GetInt32(6)

                   );


            }
            conn.Close();
            return ordertable;
        }




    }
}
