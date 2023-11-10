using Caliburn.Micro;
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TAN.EventModels;
using TAN.Helpers;
using TAN.ViewModels;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for AddParties.xaml
    /// </summary>
    public partial class AddExpenseCat : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public AddExpenseCat(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            closePage();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            closePage();
        }
        private void closePage()
        {
            //_ = _events.PublishOnUIThreadAsync(new RemoveSelectUnitEventModel());
                    
        }
        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
