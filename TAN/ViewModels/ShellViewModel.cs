﻿using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TAN.EventModels;
using TAN.Helpers;
using TAN.Views;
namespace TAN.ViewModels
{

    public class ShellViewModel : Conductor<Object>, IHandle<LogOnEventModel>,
        IHandle<AddPartyEventModel>
        , IHandle<ClearChildShellView>, IHandle<ShowSalePageEventModel>, IHandle<ClearSalePageEventModel>
        , IHandle<PaymentInEventModel>, IHandle<RemovePaymentInEventModel>
        , IHandle<PaymentOutEventModel>, IHandle<RemovePaymentOutEventModel>
        , IHandle<addCreditNotePageEventModel>, IHandle<RemoveCreditNotePageEventModel>
        , IHandle<addDebitNotePageViewEventModel>, IHandle<removeDebitViewPageViewEventModel>
        , IViewAware
    {
        private IEventAggregator _events;
        private DashBoardViewModel _dashboardVM;
        private SimpleContainer _container;
        private IAPIHelper _apiHelper;

        //For geting every method reference from ShellView
        private IShellView _view;
        protected override void OnViewAttached(object view, object context)
        {
            _view = view as IShellView;
        }




        [Obsolete]
        public ShellViewModel(IEventAggregator eventAggregator
            , DashBoardViewModel dashBoardViewModel
            , SimpleContainer simpleContainer, IAPIHelper aPIHelper)
        {
            _events = eventAggregator;
            _dashboardVM = dashBoardViewModel;
            _container = simpleContainer;
            _apiHelper = aPIHelper;

            _events.Subscribe(this);

            _ = FirstMethod();



        }

        public async Task FirstMethod()
        {
            var temp = await commanHelper.checkIfUserAlreadyLogin(_apiHelper);
            if (temp)
            {
                _ = ActivateItemAsync(_dashboardVM);
            }
            else
            {
                _ = ActivateItemAsync(_container.GetInstance<LoginViewModel>());
            }
        }

        public Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            ActivateItemAsync(_dashboardVM);
            return Task.CompletedTask;
        }

        public Task HandleAsync(AddPartyEventModel message, CancellationToken cancellationToken)
        {
            _view.addPartiesInShellView(_events, _apiHelper);
            return Task.CompletedTask;
        }

        Task IHandle<ClearChildShellView>.HandleAsync(ClearChildShellView message, CancellationToken cancellationToken)
        {
            _view.clearChildOfShellView();
            return Task.CompletedTask;
        }

        public Task HandleAsync(ShowSalePageEventModel message, CancellationToken cancellationToken)
        {
            _view.addSalePagetoShellView(_events, _apiHelper, message.orderType);
            return Task.CompletedTask;
        }

        public Task HandleAsync(ClearSalePageEventModel message, CancellationToken cancellationToken)
        {
            _view.removeSalePagefromShellView();
            return Task.CompletedTask;
        }


        public Task HandleAsync(PaymentInEventModel message, CancellationToken cancellationToken)
        {
            _view.addPaymentInShellView(_events, _apiHelper);
            return Task.CompletedTask;
        }

        public Task HandleAsync(RemovePaymentInEventModel message, CancellationToken cancellationToken)
        {
            _view.removePaymentChildOfShellView();
            return Task.CompletedTask;
        }

        public Task HandleAsync(PaymentOutEventModel message, CancellationToken cancellationToken)
        {
            _view.addPaymentOutShellView(_events, _apiHelper);
            return Task.CompletedTask;
        }

        public Task HandleAsync(RemovePaymentOutEventModel message, CancellationToken cancellationToken)
        {
            _view.removePaymentOutChildOfShellView();
            return Task.CompletedTask;
        }

        public Task HandleAsync(addCreditNotePageEventModel message, CancellationToken cancellationToken)
        {
            _view.addCreditNoteToShellView(_events, _apiHelper);
            return Task.CompletedTask;
        }

        public Task HandleAsync(RemoveCreditNotePageEventModel message, CancellationToken cancellationToken)
        {
            _view.removeCreditNoteFromShellView();
            return Task.CompletedTask;
        }

        public Task HandleAsync(addDebitNotePageViewEventModel message, CancellationToken cancellationToken)
        {
            _view.addDebitNoteToShellView(_events, _apiHelper);
            return Task.CompletedTask;
        }

        public Task HandleAsync(removeDebitViewPageViewEventModel message, CancellationToken cancellationToken)
        {
            _view.removeDebitNoteFromShellView();
            return Task.CompletedTask;
        }
    }
}
