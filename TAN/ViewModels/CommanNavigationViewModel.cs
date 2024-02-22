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
        public CommanNavigationViewModel(int n, IEventAggregator events, IAPIHelper aPIHelper )
        {
            _events = events;
            _apiHelper = aPIHelper;
            switch (n)
            {
                case 1:
                    ShowView = new saleInvoices(_events, _apiHelper, 1);
                    break;
                case 2:
                    ShowView = new saleInvoices(_events, _apiHelper, 2);
                    break;
                case 3:
                    ShowView = new PaymentInorOutAndCrorDrNoteView(_events, _apiHelper, 3);
                    break;
                case 4:
                    ShowView = new PaymentInorOutAndCrorDrNoteView(_events, _apiHelper , 4 );
                    break;
                case 5:
                    ShowView = new PaymentInorOutAndCrorDrNoteView(_events, _apiHelper, 5 );
                    break;
                case 6:
                    ShowView = new PaymentInorOutAndCrorDrNoteView(_events, _apiHelper, 6);
                    break;
                
            }
        }
    }
}
