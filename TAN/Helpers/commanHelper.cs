using DataBaseManger;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TAN.Helpers
{
    public class commanHelper
    {
        public static async Task<bool> checkIfUserAlreadyLogin(IAPIHelper _apiHelper)
        {

            if (DbConnection.DBExists())
            {
                adminModel adminData = AdminTableSqlite.getAdminData();
                var result = await _apiHelper.Authicate(adminData.adminEmail, adminData.adminPassword);
                var token = result.data1.adminToken;

                var apiVersionResponse = await _apiHelper.getAllApiVersion1(token);
                var apiVersionRemoteList = apiVersionResponse.data;
                var apiVersionLocalList = appConfigSqlite.getData();

                Dictionary<string,string> apiVersionRemote = new Dictionary<string,string>();
                foreach(var item in apiVersionRemoteList)
                {
                    apiVersionRemote.Add(item.appconfigName , item.appconfigVersion);
                }

                Dictionary<string, string> apiVersionLocal = new Dictionary<string, string>();
                foreach (var item in apiVersionLocalList)
                {
                    apiVersionLocal.Add(item.appconfigName, item.appconfigVersion);
                }

                adminData.adminToken = token;
                AdminTableSqlite.editData(adminData);


                foreach(var item in apiVersionRemote)
                {
                    if(item.Value != apiVersionLocal[item.Key])
                    {
                        validateGetRequest(item.Key, _apiHelper, token);
                        appConfigSqlite.editData(item.Key , item.Value);
                    }
                }


                return true;
                

            }
            return false;
        }
        public static void validateGetRequest(string data ,IAPIHelper _apiHelper ,string token)
        {
            switch (data)
            {
                case "customer":
                    _ = fetchAllCustomers( _apiHelper , token);
                    break;
                case "payment":
                    _ = fetchAllPayments(_apiHelper, token);
                    break;
                case "ordertable":
                    _ = fetchAllOrderTable(_apiHelper, token);
                    break;
                case "paymentType":
                    _ = fetchAllPaymentTypes(_apiHelper, token);
                    break;
                case "product":
                    _ = fetchAllProductVersions(_apiHelper, token);
                    break;
                case "orderProductRelation":
                    _ = fetchAllOrderProductRelation(_apiHelper, token);
                    break;
                case "expenseCategary":
                    _ = fetchExpenseCategory(_apiHelper, token);
                    break;
                case "expenseItem":
                    _ = fetchAllExpenseItem(_apiHelper, token);
                    break;
                case "Rl_expense_cat_item":
                    _ = fetchAllRl_expense_cat_item(_apiHelper, token);
                    break;
                case "ItemUnit":
                    _ = fetchAllItemUnit(_apiHelper, token);
                    break;
            }
        }
        public static async Task<Task> fetchAllDataAsync(IAPIHelper _apiHelper, string token, adminModel adminData)
        {
            
            
            AdminTableSqlite.addData(adminData);


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
            
            var ans11 = await _apiHelper.getAllItemUnit(token);
            foreach (var item in ans11.data)
            {
                ItemUnitSqllite.addData(item);
            }


            var ans12 = await _apiHelper.getAllApiVersion1(token);
            foreach (var item in ans12.data)
            {
                appConfigSqlite.addData(item);
            }

            return Task.CompletedTask;
        }



        public static async Task<Task> fetchAllCustomers(IAPIHelper _apiHelper, string token)
        {
            var ans = await _apiHelper.getAllCustomers(token);
            foreach (var item in ans.data)
            {
                CustomerSqllite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<Task> fetchAllPayments(IAPIHelper _apiHelper, string token)
        {
            var ans1 = await _apiHelper.getAllPayments(token);
            foreach (var item in ans1.data)
            {
                paymentSqlite.addData(item);
            }

            return Task.CompletedTask;
        }
        public static async Task<Task> fetchAllProductVersions(IAPIHelper _apiHelper, string token)
        {
            var ans3 = await _apiHelper.getAllProductVersions(token);
            foreach (var item in ans3.data)
            {
                ProductVersionModelSqlite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<Task> fetchAllOrderProductRelation(IAPIHelper _apiHelper, string token)
        {
            var ans4 = await _apiHelper.getAllorderProductRelation(token);
            foreach (var item in ans4.data)
            {
                orderProductRelationModelSqlite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<Task> fetchAllOrderTable(IAPIHelper _apiHelper, string token)
        {
            var ans5 = await _apiHelper.getAllOrderTable(token);
            foreach (var item in ans5.data)
            {
                OrderTableSqlite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<Task> fetchAllPaymentTypes(IAPIHelper _apiHelper, string token)
        {
            var ans6 = await _apiHelper.getAllPaymentTypes(token);
            foreach (var item in ans6.data)
            {
                PaymentTypeSqlite.addData(item);
            }

            return Task.CompletedTask;
        }
        public static async Task<Task> fetchExpenseCategory(IAPIHelper _apiHelper, string token)
        {
            var ans7 = await _apiHelper.getAllExpenseCategory(token);
            foreach (var item in ans7.data)
            {
                ExpenseCategorySqllite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<Task> fetchAllExpenseItem(IAPIHelper _apiHelper, string token)
        {
            var ans8 = await _apiHelper.getAllExpenseItem(token);
            foreach (var item in ans8.data)
            {
                ExpenseItemSqllite.addData(item);
            }

            return Task.CompletedTask;
        }

        public static async Task<Task> fetchAllRl_expense_cat_item(IAPIHelper _apiHelper, string token)
        {
            var ans9 = await _apiHelper.getAllRl_expense_cat_item(token);
            foreach (var item in ans9.data)
            {
                Rl_expense_cat_itemSqllite.addData(item);
            }

            return Task.CompletedTask;
        }

        

        public static async Task<Task> fetchAllItemUnit(IAPIHelper _apiHelper, string token)
        {
            var ans11 = await _apiHelper.getAllItemUnit(token);
            foreach (var item in ans11.data)
            {
                ItemUnitSqllite.addData(item);
            }

            return Task.CompletedTask;
        }




    }
}
