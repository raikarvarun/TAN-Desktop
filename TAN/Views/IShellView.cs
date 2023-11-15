using Caliburn.Micro;
using TAN.Helpers;

namespace TAN.Views
{
    public interface IShellView
    {
        void addPartiesInShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void clearChildOfShellView();

        void addSalePagetoShellView(IEventAggregator events, IAPIHelper aPIHelper , int orderType);
        void removeSalePagefromShellView();
        

        void addPaymentInShellView(IEventAggregator events, IAPIHelper aPIHelper, int orderType);
        void removePaymentChildOfShellView();

        void addSelectUnitShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeSelectUnitChildOfShellView();

        void addExpenseCategoryShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeExpenseCategoryChildOfShellView();

        void addExpenseItemShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeExpenseItemChildOfShellView();

        void addExpensePageShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeExpensePageChildOfShellView();

        void addSettingMainViewShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeSettingMainViewChildOfShellView();



    }
}