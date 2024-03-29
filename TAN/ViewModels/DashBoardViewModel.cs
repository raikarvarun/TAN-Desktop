﻿using Caliburn.Micro;
using DataBaseManger;
using DataBaseManger.Model;
using System.Windows;
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
        public RelayCommand ReportsCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        public RelayCommand SubscriptionViewCommand { get; set; }





        public RelayCommand AddSaleClicked { get; set; }
        public RelayCommand PurchaseSaleClicked { get; set; }


        public HomeViewModel HomeVM { get; set; }




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




            CurrentView = HomeVM;

            AddSaleClicked = new RelayCommand(o =>
            {
                _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(1, 1, null));
            });
            PurchaseSaleClicked = new RelayCommand(o =>
            {
                _events.PublishOnUIThreadAsync(new ShowSalePageEventModel(2, 1, null));
            });
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            PartiesViewCommand = new RelayCommand(o =>
            {
                CurrentView = new PartiesView(events, aPIHelper);
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
                CurrentView = new ExpenseView(_events, _apiHelper);

            });
            BankAccountCommand = new RelayCommand(o =>
            {
                CurrentView = new BankAccountsView(changeCurrentViewtoAddBank);

            });

            ReportsCommand = new RelayCommand(o =>
            {
                CurrentView = new ReportMainView(_events, _apiHelper);

            });

            SettingsCommand = new RelayCommand(o =>
            {
                _ = _events.PublishOnUIThreadAsync(new AddSettingMainEventModel());

            });
            LogoutCommand = new RelayCommand(o =>
            {
                Logout();

            });

            SubscriptionViewCommand = new RelayCommand(o =>
            {
                CurrentView = new SubscriptionView(_events, _apiHelper);

            });
        }
        private void Logout()
        {
            string msgtext = "You want to logout. Do you want to continue?";
            string txt = "TAN NIBM";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.OK:
                    DbConnection.deleteDb();
                    _ = _events.PublishOnUIThreadAsync(new LogInEventModel());
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }
        public void changeCurrentViewtoAddUnits()
        {
            CurrentView = new AddUnitVIew(_events, _apiHelper, changeCurrentViewtoUnits);
        }
        public void changeCurrentViewtoUnits()
        {
            CurrentView = new ItemMainVIew(1, _events, changeCurrentViewtoAddItems, changeCurrentViewtoAddUnits);
        }


        public void changeCurrentViewtoAddItems(int whichMode, productVersionModel data)
        {
            CurrentView = new AddItemView(_events, _apiHelper, changeCurrentViewtoItems, whichMode, data);
        }
        public void changeCurrentViewtoItems()
        {
            CurrentView = new ItemMainVIew(0, _events, changeCurrentViewtoAddItems, changeCurrentViewtoAddUnits);
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
