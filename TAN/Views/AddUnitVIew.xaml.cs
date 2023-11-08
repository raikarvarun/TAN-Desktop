﻿using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for AddItemView.xaml
    /// </summary>
    public partial class AddUnitVIew : UserControl
    {
        public delegate void TEmpfun();
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;

        private TEmpfun _changeCurrentViewtoItems;
        public AddUnitVIew(IEventAggregator events, IAPIHelper aPIHelper, TEmpfun re)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
            _changeCurrentViewtoItems = re;
        }


        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            var unitName = UnitName.Text;
            var shortName = ShortName.Text;



            var temp =  appConfigSqlite.getData();
            var token = temp.adminToken;

            //var data = new ;

            //var ans = await _apiHelper.postProducts(token, data);
            //ProductVersionModelSqlite.addData(ans.data);
            _changeCurrentViewtoItems();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _changeCurrentViewtoItems();
        }

        private void SaveNewButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}