using Caliburn.Micro;
using TAN.Core;
using TAN.EventModels;

namespace TAN.ViewModels
{

    public class PartiesViewModel : Screen
    {
        //public static ObservableCollection<customerModel> PARTIES { get; set; }
        private IEventAggregator _events;
        public RelayCommand SaveButtonClicked { get; set; }
        public PartiesViewModel(IEventAggregator events)
        {
            _events = events;
            SaveButtonClicked = new RelayCommand(o =>
            {
                _events.PublishOnUIThreadAsync(new AddPartyEventModel());
                
            });

        }



    }
}
