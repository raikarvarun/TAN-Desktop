using DataBaseManger.Model;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TAN.Models;
using TAN.PostReqResponse;
using TAN.PostRequest;

namespace TAN.Helpers
{
    public class Comman<T>
    {


        public async Task<GetAllCommanResponse<T>> GetAll(string token, string url1, HttpClient ApiClient)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += url1;
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<GetAllCommanResponse<T>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<PostReqCommanResponse<T>> PostData(string token, string url1, T newData, HttpClient ApiClient)
        {



            var content = JsonConvert.SerializeObject(newData);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var data = new ByteArrayContent(buffer);

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += url1;
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PostReqCommanResponse<T>>();


                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<PostReqCommanResponse<T>> UpdateData(string token, string url1, T newData, HttpClient ApiClient)
        {



            var content = JsonConvert.SerializeObject(newData);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var data = new ByteArrayContent(buffer);

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += url1;
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PutAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PostReqCommanResponse<T>>();


                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<PostReqCommanResponse<T>> DeleteData(string url1, Dictionary<string, string> query, HttpClient ApiClient)
        {






            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += url1;
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.DeleteAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PostReqCommanResponse<T>>();


                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<GetAllCommanResponse<T>> SendBills(string token, string url1, HttpClient ApiClient)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += url1;
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<GetAllCommanResponse<T>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }

    public class APIHelper : IAPIHelper
    {

        public HttpClient ApiClient { get; set; }

        public APIHelper()
        {
            InitilizeClient();
        }
        private void InitilizeClient()
        {

            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<adminResponse> Authicate(string username, string password)
        {
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/admin/login";
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("adminEmail", username),
                new KeyValuePair<string, string>("adminPassword", password)
            });
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<adminResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }





        //
        //
        //
        // Get Request
        public Task<GetAllCommanResponse<adminModel>> getAllAdminTable(string token)
        {
            Comman<adminModel> comman = new Comman<adminModel>();
            return comman.GetAll(token, "/api/admintable/all", ApiClient);
        }

        public Task<GetAllCommanResponse<appConfigModel>> getAllApiVersion1(string token)
        {
            Comman<appConfigModel> comman = new Comman<appConfigModel>();
            return comman.GetAll(token, "/api/appconfig/all", ApiClient);
        }


        public Task<GetAllCommanResponse<customerModel>> getAllCustomers(string token)
        {
            Comman<customerModel> comman = new Comman<customerModel>();
            return comman.GetAll(token, "/api/customer/all", ApiClient);
        }

        public Task<GetAllCommanResponse<paymentModel>> getAllPayments(string token)
        {
            Comman<paymentModel> comman = new Comman<paymentModel>();
            return comman.GetAll(token, "/api/payment/all", ApiClient);
        }

        public Task<GetAllCommanResponse<productVersionModel>> getAllProductVersions(string token)
        {
            Comman<productVersionModel> comman = new Comman<productVersionModel>();
            return comman.GetAll(token, "/api/productversion/all", ApiClient);
        }

        public Task<GetAllCommanResponse<orderProductRelationModel>> getAllorderProductRelation(string token)
        {
            Comman<orderProductRelationModel> comman = new Comman<orderProductRelationModel>();
            return comman.GetAll(token, "/api/orderproductrelation/all", ApiClient);
        }

        public Task<GetAllCommanResponse<ExpenseCategoryModel>> getAllExpenseCategory(string token)
        {
            Comman<ExpenseCategoryModel> comman = new Comman<ExpenseCategoryModel>();
            return comman.GetAll(token, "/api/expensecat/all", ApiClient);
        }

        public Task<GetAllCommanResponse<ExpenseItemModel>> getAllExpenseItem(string token)
        {
            Comman<ExpenseItemModel> comman = new Comman<ExpenseItemModel>();
            return comman.GetAll(token, "/api/expenseitem/all", ApiClient);
        }

        public Task<GetAllCommanResponse<Rl_expense_cat_itemModel>> getAllRl_expense_cat_item(string token)
        {
            Comman<Rl_expense_cat_itemModel> comman = new Comman<Rl_expense_cat_itemModel>();
            return comman.GetAll(token, "/api/rlexpensecatitem/all", ApiClient);
        }

        public Task<GetAllCommanResponse<ItemUnitModel>> getAllItemUnit(string token)
        {
            Comman<ItemUnitModel> comman = new Comman<ItemUnitModel>();
            return comman.GetAll(token, "/api/itemunit/all", ApiClient);
        }



        public Task<GetAllCommanResponse<OrderTableModel>> getAllOrderTable(string token)
        {
            Comman<OrderTableModel> comman = new Comman<OrderTableModel>();
            return comman.GetAll(token, "/api/order/all", ApiClient);
        }

        public Task<GetAllCommanResponse<PaymentTypeModel>> getAllPaymentTypes(string token)
        {
            Comman<PaymentTypeModel> comman = new Comman<PaymentTypeModel>();
            return comman.GetAll(token, "/api/paymenttype/all", ApiClient);
        }



        //
        //
        //
        //Post Request

        public async Task<PostReqCommanResponse<customerModel>> postCustomers(string token, customerModel customer)
        {
            Comman<customerModel> comman = new Comman<customerModel>();
            return await comman.PostData(token, "/api/customer/insert", customer, ApiClient);

        }

        public async Task<PostReqCommanResponse<productVersionModel>> postProducts(string token, productVersionModel product)
        {
            Comman<productVersionModel> comman = new Comman<productVersionModel>();
            return await comman.PostData(token, "/api/productversion/insert", product, ApiClient);

        }

        public async Task<PostReqCommanResponse<PaymentTypeModel>> postPaymentType(string token, PaymentTypeModel paymentType)
        {
            Comman<PaymentTypeModel> comman = new Comman<PaymentTypeModel>();
            return await comman.PostData(token, "/api/paymenttype/insert", paymentType, ApiClient);

        }

        public Task<PostReqCommanResponse<ItemUnitModel>> postItemUnit1(string token, ItemUnitModel itemUnit)
        {
            Comman<ItemUnitModel> comman = new Comman<ItemUnitModel>();
            return comman.PostData(token, "/api/itemunit/insert", itemUnit, ApiClient);

        }

        public Task<PostReqCommanResponse<ExpenseCategoryModel>> postExpenseCat(string token, ExpenseCategoryModel data)
        {
            Comman<ExpenseCategoryModel> comman = new Comman<ExpenseCategoryModel>();
            return comman.PostData(token, "/api/expensecat/insert", data, ApiClient);

        }
        public Task<PostReqCommanResponse<ExpenseItemModel>> postExpenseItem(string token, ExpenseItemModel data)
        {
            Comman<ExpenseItemModel> comman = new Comman<ExpenseItemModel>();
            return comman.PostData(token, "/api/expenseitem/insert", data, ApiClient);

        }

        public Task<PostReqCommanResponse<adminModel>> postAdminTable(string token, adminModel data)
        {
            Comman<adminModel> comman = new Comman<adminModel>();
            return comman.PostData(token, "/api/admintable/insert", data, ApiClient);

        }

        public async Task<PlaceOrderPostResponse> postPlaceOrder1(string token, PlaceOrderPostRequest PlaceOrder)
        {

            var content = JsonConvert.SerializeObject(PlaceOrder);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var data = new ByteArrayContent(buffer);
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/placeorder/insert";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PlaceOrderPostResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);

                }
            }
        }

        //
        //
        //
        // Put Request
        public async Task<PostReqCommanResponse<customerModel>> editCustomer(string token, customerModel customer)
        {
            Comman<customerModel> comman = new Comman<customerModel>();
            var url = "/api/customer/update/" + customer.customerID;
            return await comman.UpdateData(token, url, customer, ApiClient);

        }
        public async Task<PostReqCommanResponse<productVersionModel>> editProduct(string token, productVersionModel product)
        {
            Comman<productVersionModel> comman = new Comman<productVersionModel>();
            var url = "/api/productversion/update/" + product.productNo;

            return await comman.UpdateData(token, url, product, ApiClient);

        }




        //
        //
        //
        //delete

        public async Task<PostReqCommanResponse<customerModel>> deleteSubscription(string token, string orderID, string PaymentID)
        {
            Comman<customerModel> comman = new Comman<customerModel>();
            var query = new Dictionary<string, string>()
            {
                ["token"] = token,
                ["orderid"] = orderID,
                ["paymentid"] = PaymentID

            };
            var url = "/api/subscription/deletesubscription";
            return await comman.DeleteData(url, query, ApiClient);

        }


    }
}
