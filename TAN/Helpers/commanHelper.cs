using DataBaseManger;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System;
using System.Threading.Tasks;

namespace TAN.Helpers
{
    public class commanHelper
    {
        public static async Task<Task> fetchAllDataAsync(IAPIHelper _apiHelper, string token, appConfigModel appConfig)
        {
            
            
            appConfigSqlite.addData(appConfig);


            var ans = await _apiHelper.getAllCustomers(token);
            foreach (var item in ans.data)
            {
                CustomerSqllite.addData(item);
            }
            var ans1 = await _apiHelper.getAllPayments(token);
            foreach (var item in ans1.data)
            {
                paymentSqlite.addData(item);
            }




            var ans3 = await _apiHelper.getAllProductVersions(token);
            foreach (var item in ans3.data)
            {
                ProductVersionModelSqlite.addData(item);
            }

            var ans4 = await _apiHelper.getAllorderProductRelation(token);
            foreach (var item in ans4.data)
            {
                orderProductRelationModelSqlite.addData(item);
            }

            var ans5 = await _apiHelper.getAllOrderTable(token);
            foreach (var item in ans5.data)
            {
                OrderTableSqlite.addData(item);
            }

            var ans6 = await _apiHelper.getAllPaymentTypes(token);
            foreach (var item in ans6.data)
            {
                PaymentTypeSqlite.addData(item);
            }


            var ans7 = await _apiHelper.getAllExpenseCategory(token);
            foreach (var item in ans7.data)
            {
                ExpenseCategorySqllite.addData(item);
            }

            var ans8 = await _apiHelper.getAllExpenseItem(token);
            foreach (var item in ans8.data)
            {
                ExpenseItemSqllite.addData(item);
            }

            var ans9 = await _apiHelper.getAllRl_expense_cat_item(token);
            foreach (var item in ans9.data)
            {
                Rl_expense_cat_itemSqllite.addData(item);
            }
            var ans10 = await _apiHelper.getAllItemMap(token);
            foreach (var item in ans10.data)
            {
                ItemMapSqllite.addData(item);
            }
            var ans11 = await _apiHelper.getAllItemUnit(token);
            foreach (var item in ans11.data)
            {
                ItemUnitSqllite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<bool> checkIfUserAlreadyLogin(IAPIHelper _apiHelper)
        {

            if (DbConnection.DBExists())
            {
                appConfigModel appConfig = appConfigSqlite.getData();
                var result = await _apiHelper.Authicate(appConfig.adminEmail, appConfig.adminPassword);
                var token = result.data1.adminToken;

                var apiVersionRemote = await _apiHelper.getApiVersion(token);
                var apiVersionLocal = appConfigSqlite.getData();

                appConfig.adminToken = token;
                appConfig.apiVersion = apiVersionRemote.data.appVersion;

                appConfigSqlite.editData(appConfig);

                if (apiVersionLocal.apiVersion == apiVersionRemote.data.appVersion)
                {
                    //PartiesViewModel.assignParties();

                    return true;
                }
                else
                {
                    try
                    {
                        

                        DbConnection.deleteDb();
                        CommanSQlite.initAll();
                        var temp3 = await fetchAllDataAsync(_apiHelper, token, appConfig);

                        //PartiesViewModel.assignParties();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }

            }
            return false;
        }
    }
}
