using DataBaseManger.Model;

namespace TAN.EventModels
{
    public class AddPartyEventModel
    {
        public int whichMode { get; set; }
        public customerModel SelectedcustomerData { get; set; }

        public AddPartyEventModel(int data, customerModel data1)
        {
            whichMode = data;
            SelectedcustomerData = data1;
        }

    }
}
