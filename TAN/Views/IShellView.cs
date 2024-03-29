﻿using Caliburn.Micro;
using DataBaseManger.Model;
using TAN.Helpers;
using TAN.PostRequest;

namespace TAN.Views
{
    public interface IShellView
    {
        void addPartiesInShellView(IEventAggregator events, IAPIHelper aPIHelper, int whichMode, customerModel customerData);
        void clearChildOfShellView();

        void addSalePagetoShellView(IEventAggregator events, IAPIHelper aPIHelper, int orderType, int whichMode, OrderTableModel SelectedData);
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

        void addAddUserViewShellView(IEventAggregator events, IAPIHelper aPIHelper);
        void removeAddUserViewChildOfShellView();

    }
}