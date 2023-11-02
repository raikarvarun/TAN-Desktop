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
        

        void addPaymentInShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removePaymentChildOfShellView();

        void addPaymentOutShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removePaymentOutChildOfShellView();

        void addCreditNoteToShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeCreditNoteFromShellView();

        void addDebitNoteToShellView(IEventAggregator events, IAPIHelper aPIHelper);

        void removeDebitNoteFromShellView();

    }
}