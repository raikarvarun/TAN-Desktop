using Caliburn.Micro;
using TAN.Core;
using TAN.EventModels;
using TAN.Helpers;
using TAN.Views;

namespace TAN.ViewModels
{
    public class DashBoardViewModel : ObservableObject
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand PartiesViewCommand { get; set; }

        public RelayCommand ItemViewCommand { get; set; }
        public RelayCommand saleInvoicesCommand { get; set; }
        public RelayCommand PurchaseBillsCommand { get; set; }

        public RelayCommand PaymentInCommand { get; set; }

        public RelayCommand SaleReturnCommand { get; set; }

        public RelayCommand PaymentOutCommand { get; set; }

        public RelayCommand PurchaseReturnCommand { get; set; }
        public RelayCommand BankAccountCommand { get; set; }

        public RelayCommand ExpenseCommand { get; set; }




        public RelayCommand AddSaleClicked { get; set; }
        public RelayCommand PurchaseSaleClicked { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public PartiesViewModel PartiesVM { get; set; }
       


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                onPropertyChanged();
            }
        }


        public DashBoardViewModel(IEventAggregator events, IAPIHelper aPIHelper)
        {
            _events = events;
            _apiHelper = aPIHelper;
            HomeVM = new HomeViewModel();
            PartiesVM = new PartiesViewModel(events);
            


            CurrentView = HomeVM;

            AddSaleClicked = new RelayCommand(o =>
            {
                _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(1));
            });
            PurchaseSaleClicked = new RelayCommand(o =>
            {
                _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(2));
            });
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            PartiesViewCommand = new RelayCommand(o =>
            {
                CurrentView = PartiesVM;
            });

            ItemViewCommand = new RelayCommand(o =>
            {
                CurrentView = new ItemMainVIew(0, _events, changeCurrentViewtoAddItems, changeCurrentViewtoAddUnits);
            });

            saleInvoicesCommand = new RelayCommand(o =>
            {

                CurrentView = new CommanNavigationViewModel(1, _events, _apiHelper);
            });
            PurchaseBillsCommand = new RelayCommand(o =>
            {

                CurrentView = new CommanNavigationViewModel(2, _events, _apiHelper);
            });

            PaymentInCommand = new RelayCommand(o =>
            {

                CurrentView = new CommanNavigationViewModel(3, _events, _apiHelper);
            });
            PaymentOutCommand = new RelayCommand(o =>
            {


                CurrentView = new CommanNavigationViewModel(4, _events, _apiHelper);

            });
            SaleReturnCommand = new RelayCommand(o =>
            {
                CurrentView = new CommanNavigationViewModel(5, _events, _apiHelper);

            });
            
            PurchaseReturnCommand = new RelayCommand(o =>
            {
                CurrentView = new CommanNavigationViewModel(6, _events, _apiHelper);

            });
            ExpenseCommand = new RelayCommand(o =>
            {
                CurrentView = new ExpenseView(_events , _apiHelper);

            });
            BankAccountCommand = new RelayCommand(o =>
            {
                CurrentView = new BankAccountsView(changeCurrentViewtoAddBank);

            });
        }
        public void changeCurrentViewtoAddUnits()
        {
            CurrentView = new AddUnitVIew(_events, _apiHelper, changeCurrentViewtoUnits);
        }
        public void changeCurrentViewtoUnits()
        {
            CurrentView = new ItemMainVIew(1,_events, changeCurrentViewtoItems , changeCurrentViewtoAddUnits);
        }


        public void changeCurrentViewtoAddItems()
        {
            CurrentView = new AddItemView(_events, _apiHelper, changeCurrentViewtoItems);
        }
        public void changeCurrentViewtoItems()
        {
            CurrentView = new ItemMainVIew(0,_events, changeCurrentViewtoAddItems , changeCurrentViewtoAddUnits);
        }
        public void changeCurrentViewtoAddBank()
        {
            CurrentView = new AddBankAccountView(_events, _apiHelper, changeCurrentViewtoBankView);
        }
        public void changeCurrentViewtoBankView()
        {
            CurrentView = new BankAccountsView(changeCurrentViewtoAddBank);
        }
    }
}
