using Caliburn.Micro;
using TAN.Core;
using TAN.Helpers;
using TAN.Views;

namespace TAN.ViewModels
{
    public class CommanNavigationViewModel : ObservableObject
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public RelayCommand HomeViewCommand { get; set; }


        private object _showView;

        public object ShowView
        {
            get { return _showView; }
            set
            {
                _showView = value;
                onPropertyChanged();
            }
        }
        public CommanNavigationViewModel(int n, IEventAggregator events, IAPIHelper aPIHelper)
        {
            _events = events;
            _apiHelper = aPIHelper;
            switch (n)
            {
                case 0:
                    ShowView = new PaymentInView(_events);
                    break;
                case 1:
                    ShowView = new saleInvoices(_events, _apiHelper);
                    break;
                case 2:
                    ShowView = new PaymentOutView(_events, _apiHelper);
                    break;
                case 3:
                    ShowView = new CreditNoteView(_events, _apiHelper);
                    break;
                case 4:
                    ShowView = new DebitNoteView(_events, _apiHelper);
                    break;
                case 5:
                    ShowView = new PurchaseBillsView(_events, _apiHelper);
                    break;
            }
        }
    }
}
