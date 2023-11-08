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





    }
}