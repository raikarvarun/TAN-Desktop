using DataBaseManger.Model;
using System.Threading.Tasks;
using TAN.Models;
using TAN.PostReqResponse;
using TAN.PostRequest;

namespace TAN.Helpers
{
    public interface IAPIHelper
    {
        Task<adminResponse> Authicate(string username, string password);
        Task<customerResponse> getAllCustomers(string token);

        Task<paymentResponse> getAllPayments(string token);


        Task<productVersionResponse> getAllProductVersions(string token);

        Task<orderProductRelationResponse> getAllorderProductRelation(string token);


        Task<apiVersionResponse> getApiVersion(string token);
        Task<customerPostResponse> postCustomers(string token, customerModel customer);
        Task<OrderTableResponse> getAllOrderTable(string token);
        Task<productPostResponse> postProducts(string token, productVersionModel product);

        Task<PlaceOrderPostResponse> postPlaceOrder(string token, PlaceOrderPostRequest PlaceOrder);
        Task<PaymentTypeResponse> getAllPaymentTypes(string token);

        Task<PaymentTypePostResponse> postPaymentType(string token, PaymentTypeModel paymentType);
    }
}