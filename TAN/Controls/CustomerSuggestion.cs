using DataBaseManger.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace TAN.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TAN.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TAN.Controls;assembly=TAN.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomerSuggestion/>
    ///
    /// </summary>
    [TemplatePart(Name = "CustomerSuggestions", Type = typeof(ListBox))]
    [TemplatePart(Name = "CustomerSuggestionPopup", Type = typeof(Popup))]

    public class CustomerSuggestion : Control
    {
        static CustomerSuggestion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomerSuggestion), new FrameworkPropertyMetadata(typeof(CustomerSuggestion)));
        }
        public DataGrid CustomerSuggestions;
        public Popup CustomerSuggestionPopup;
        TextBox _textbox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                CustomerSuggestions = this.Template.FindName("CustomerSuggestions", this) as DataGrid;
                CustomerSuggestionPopup = this.Template.FindName("CustomerSuggestionPopup", this) as Popup;
                CustomerSuggestions.PreviewMouseDown += (s, e) =>
                {
                    SuggestionsPreviewMouseDown(e);
                };


            }
        }

        private void SuggestionskeyDown(KeyEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {
                ListBoxItem listBoxItem = e.OriginalSource as ListBoxItem;
                _textbox.Text = listBoxItem.Content as string;

                if (e.Key == Key.Enter)
                {
                    CustomerSuggestionPopup.IsOpen = false;
                }
            }
        }

        private void SuggestionsPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                customerModel temp = (customerModel)CustomerSuggestions.SelectedItem;
                if (temp != null)
                {
                    _textbox.Text = temp.customerName;
                    onSlelevte(temp);
                    CustomerSuggestionPopup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        //internal void ShowSuggestion(ObservableCollection<string> listSuggestion, TextBox textBox)
        //{

        //    _textbox = textBox;
        //    CustomerSuggestions.Items.Clear();



        //    if (listSuggestion != null)
        //        foreach (var each in listSuggestion)
        //            CustomerSuggestions.Items.Add(each);



        //    CustomerSuggestionPopup.IsOpen = CustomerSuggestions.Items.Count > 0;
        //    CustomerSuggestions.Items.Filter = f =>
        //    {
        //        string _text = f as string;
        //        return _text.StartsWith(_textbox.Text, StringComparison.CurrentCultureIgnoreCase) &&
        //        !(string.Equals(_text, _textbox.Text, StringComparison.CurrentCultureIgnoreCase));
        //    };

        //}

        public delegate void TEmpfun(customerModel data);

        private TEmpfun onSlelevte;
        public void AssginFunction(TEmpfun temp)
        {
            onSlelevte = temp;
        }


        //private List<customerModel> _products;
        public void AssginParties(TextBox searchTextBox)
        {
            _textbox = searchTextBox;
            CustomerSuggestionPopup.IsOpen = true;

            string searchKey = searchTextBox.Text.ToLower();
            if (searchKey == "")
                CustomerSuggestions.ItemsSource = _customerMainData.Take(40);
            else
                CustomerSuggestions.ItemsSource = _customerMainData.Where(s => s.customerName.ToLower().Contains(searchKey)).ToList().Take(30);

        }
        private List<customerModel> _customerMainData;
        public void AssginCustomerMainData(List<customerModel> productMainData)
        {
            _customerMainData = productMainData;
        }
        internal void LostFocuss(RoutedEventArgs e)
        {

            CustomerSuggestionPopup.IsOpen = false;
        }
    }
}
