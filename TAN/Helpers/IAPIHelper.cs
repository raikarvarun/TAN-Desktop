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
        Task<GetAllCommanResponse<customerModel>> getAllCustomers(string token);

        Task<GetAllCommanResponse<adminModel>> getAllAdminTable(string token);

        Task<GetAllCommanResponse<paymentModel>> getAllPayments(string token);


        Task<GetAllCommanResponse<productVersionModel>> getAllProductVersions(string token);

        Task<GetAllCommanResponse<orderProductRelationModel>> getAllorderProductRelation(string token);


        Task<GetAllCommanResponse<appConfigModel>> getAllApiVersion1(string token);
        Task<PostReqCommanResponse<customerModel>> postCustomers(string token, customerModel customer);
        Task<GetAllCommanResponse<OrderTableModel>> getAllOrderTable(string token);
        Task<PostReqCommanResponse<productVersionModel>> postProducts(string token, productVersionModel product);

        Task<PlaceOrderPostResponse> postPlaceOrder1(string token, PlaceOrderPostRequest PlaceOrder);
        Task<GetAllCommanResponse<PaymentTypeModel>> getAllPaymentTypes(string token);

        Task<PostReqCommanResponse<PaymentTypeModel>> postPaymentType(string token, PaymentTypeModel paymentType);
        

        Task<GetAllCommanResponse<ExpenseCategoryModel>> getAllExpenseCategory(string token);
        Task<GetAllCommanResponse<ExpenseItemModel>> getAllExpenseItem(string token);
        Task<GetAllCommanResponse<Rl_expense_cat_itemModel>> getAllRl_expense_cat_item(string token);
        Task<GetAllCommanResponse<ItemUnitModel>> getAllItemUnit(string token);

        Task<PostReqCommanResponse<ItemUnitModel>> postItemUnit1(string token, ItemUnitModel itemUnit);

        Task<PostReqCommanResponse<ExpenseCategoryModel>> postExpenseCat(string token, ExpenseCategoryModel data);

        Task<PostReqCommanResponse<ExpenseItemModel>> postExpenseItem(string token, ExpenseItemModel data);

        Task<PostReqCommanResponse<adminModel>> postAdminTable(string token, adminModel data);


    }
}