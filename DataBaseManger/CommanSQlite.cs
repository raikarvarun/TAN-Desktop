using DataBaseManger.SqlLite;
using System.Globalization;

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
            ExpenseCategorySqllite.createTable();
            ExpenseItemSqllite.createTable();
            Rl_expense_cat_itemSqllite.createTable() ;
            ItemUnitSqllite.createTable();
            ItemMapSqllite.createTable();
        }
        public static string DisplayIndianCurrency(string data)
        {
            decimal parsed = decimal.Parse(data, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            hindi.NumberFormat.CurrencySymbol = "";
            string text = string.Format(hindi, "{0:c}", parsed);
            return text;
        }
    }
}
