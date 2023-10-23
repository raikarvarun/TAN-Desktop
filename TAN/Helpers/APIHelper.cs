using DataBaseManger.Model;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
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
        public async Task<apiVersionResponse> getApiVersion(string token)
        {

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/appconfig/get";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<apiVersionResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<customerResponse> getAllCustomers(string token)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/customer/all";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<customerResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<paymentResponse> getAllPayments(string token)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/payment/all";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<paymentResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }



        public async Task<productVersionResponse> getAllProductVersions(string token)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/productversion/all";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<productVersionResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<orderProductRelationResponse> getAllorderProductRelation(string token)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/orderproductrelation/all";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<orderProductRelationResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<customerPostResponse> postCustomers(string token, customerModel customer)
        {

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("customerName", customer.customerName),
                new KeyValuePair<string, string>("customerTypeID", customer.customerTypeID.ToString()),
                new KeyValuePair<string, string>("customerMobile", customer.customerMobile),
                new KeyValuePair<string, string>("customerAddress", customer.customerAddress),
                new KeyValuePair<string, string>("customerOpeningAmount", customer.customerOpeningAmount.ToString()),
                new KeyValuePair<string, string>("TotalAmount", customer.TotalAmount.ToString())
            });

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/customer/insert";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<customerPostResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<productPostResponse> postProducts(string token, productVersionModel product)
        {

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("productType", product.productType),
                new KeyValuePair<string, string>("productPrice", product.productPrice.ToString()),
                new KeyValuePair<string, string>("productQuntity", product.productQuntity.ToString()),
                new KeyValuePair<string, string>("productImage", product.productImage),
                new KeyValuePair<string, string>("productName", product.productName.ToString()),
                new KeyValuePair<string, string>("openingQuantity", product.openingQuantity.ToString()),
                new KeyValuePair<string, string>("atprice", product.atprice.ToString()),
                new KeyValuePair<string, string>("salePrice", product.salePrice.ToString()),
                new KeyValuePair<string, string>("purchasePrice", product.purchasePrice.ToString())

            });

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/productversion/insert";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<productPostResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<PaymentTypePostResponse> postPaymentType(string token, PaymentTypeModel paymentType)
        {

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("paymentTypeType", paymentType.paymentTypeType),
                new KeyValuePair<string, string>("paymentTypeName", paymentType.paymentTypeName)


            });

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/paymenttype/insert";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PaymentTypePostResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<OrderTableResponse> getAllOrderTable(string token)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/order/all";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<OrderTableResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<PaymentTypeResponse> getAllPaymentTypes(string token)
        {
            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/paymenttype/all";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PaymentTypeResponse>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<PlaceOrderPostResponse> postPlaceOrder(string token, PlaceOrderPostRequest PlaceOrder)
        {

            var content = JsonConvert.SerializeObject(PlaceOrder);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var query = new Dictionary<string, string>()
            {
                ["token"] = token

            };
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiUrl += "/api/placeorder/insert";
            apiUrl = QueryHelpers.AddQueryString(apiUrl, query);
            using (HttpResponseMessage response = await ApiClient.PostAsync(apiUrl, byteContent))
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
    }
}
