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
using TAN.EventModels;
using TAN.Helpers;

namespace TAN.Views.SettingViews
{
    /// <summary>
    /// Interaction logic for UserMainView.xaml
    /// </summary>
    public partial class UserMainView : UserControl
    {
        private IEventAggregator _events;
        private IAPIHelper _apiHelper;
        public UserMainView(IEventAggregator events, IAPIHelper aPIHelper)
        {
            InitializeComponent();
            _events = events;
            _apiHelper = aPIHelper;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = _events.PublishOnUIThreadAsync(new AddUserViewEventModel());
        }
    }
}
