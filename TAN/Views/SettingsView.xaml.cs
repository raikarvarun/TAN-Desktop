using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TAN.Helpers;
using TAN.Views.SettingViews;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public SettingsView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;

            PrevButton = GeneralButton;
            PrevButton.Foreground = Brushes.Black;
            PrevButton.Background = new SolidColorBrush(Color.FromArgb(255, 250, 250, 255));
            PrevButton.FontWeight = FontWeights.Medium;
        }

        Button PrevButton;
        private void ChangeSelection(object sender, RoutedEventArgs e)
        {

           
            PrevButton.Background = Brushes.Transparent;
            PrevButton.Foreground = Brushes.LightGray;
            PrevButton.FontWeight = FontWeights.Regular;



            Button currButton = sender as Button;
            
            currButton.Background = new SolidColorBrush(Color.FromArgb(255, 250, 250, 255));
            currButton.Foreground = Brushes.Black;
            currButton.FontWeight = FontWeights.Medium;


            PrevButton = currButton;


        }

        private void GeneralButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
        }

        private void TransactionButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
        }

        private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);
            MainWindow.Content = new UserMainView(_events , _apiHelper);
        }

        private void PartyButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelection(sender, e);

        }
    }
}
