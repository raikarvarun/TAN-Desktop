using DataBaseManger.SqlLite;

namespace DataBaseManger
{
    public class CommanSQlite
    {
        public static void initAll()
        {
            CustomerSqllite.createTable();
            paymentSqlite.createTable();
            ProductVersionModelSqlite.createTable();
            orderProductRelationModelSqlite.createTable();
            appConfigSqlite.createTable();
            OrderTableSqlite.createTable();
            PaymentTypeSqlite.createTable();
        }
    }
}
