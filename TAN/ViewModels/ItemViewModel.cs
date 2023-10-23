using Caliburn.Micro;
using TAN.Core;

namespace TAN.ViewModels
{
    public class ItemViewModel : Screen
    {
        public delegate void TEmpfun();
        private IEventAggregator _events;
        public RelayCommand SaveButtonClicked { get; set; }
        public ItemViewModel(IEventAggregator events, TEmpfun tempfun)
        {
            _events = events;
            SaveButtonClicked = new RelayCommand(o =>
            {
                tempfun();
            });

        }

    }
}
