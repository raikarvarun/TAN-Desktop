using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TAN.EventModels;

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
    ///     <MyNamespace:CustomerSuggestionTextBox/>
    ///
    /// </summary>
    [TemplatePart(Name = "CustomerSuggestionEditor", Type = typeof(TextBox))]
    public class CustomerSuggestionTextBox : Control
    {
        public delegate void CustomerChangeEventHandler(object sender, CustomerDataEventArgs args);
        public delegate void AddCustomerEventHandler(object sender, AddCustomerEventArgs args);

        private customerModel _customerData { get; set; }

        static CustomerSuggestionTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomerSuggestionTextBox), new FrameworkPropertyMetadata(typeof(CustomerSuggestionTextBox)));
        }
        TextBox textBox;
        CustomerSuggestion suggestion;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("CustomerSuggestionEditor", this) as TextBox;
                suggestion = this.Template.FindName("CustomerSuggestion", this) as CustomerSuggestion;
                if (textBox != null)
                {
                    List<customerModel> CustomerMainData = CustomerSqllite.readAll();
                    suggestion.AssginCustomerMainData(CustomerMainData);
                    suggestion.AssginFunction(OnCustomerSeleted, OnAddPartySelected);
                    textBox.PreviewKeyDown += (s, e) => { TextBoxPreviewKeyDown(e); };
                    textBox.KeyDown += (s, e) => { TextBoxKeyDown(e); };
                    textBox.TextChanged += (s, e) => { suggestion.AssginParties(textBox); };
                    textBox.GotFocus += (s, e) => { TextBoxMouseDown(); };
                    //textBox.LostFocus += (s, e) => { suggestion.LostFocuss(e); };

                }
            }
        }

        private void TextBoxMouseDown()
        {
            if (!suggestion.CustomerSuggestionPopup.IsOpen) 
            {
                suggestion.CustomerSuggestionPopup.IsOpen = true;
                suggestion.AssginParties(textBox);
            }
                

        }


        //Customer selected event
        public static RoutedEvent CustomerSelectedEvent = EventManager.RegisterRoutedEvent("CustomerSeleted",
             RoutingStrategy.Bubble, typeof(CustomerChangeEventHandler), typeof(CustomerSuggestionTextBox));

        public event CustomerChangeEventHandler CustomerSeleted
        {
            add
            {
                AddHandler(CustomerSelectedEvent, value);
            }
            remove
            {
                RemoveHandler(CustomerSelectedEvent, value);
            }
        }


        protected virtual void OnCustomerSeleted(customerModel data)
        {

            setcustomerData(data);
            RaiseEvent(new CustomerDataEventArgs(CustomerSelectedEvent, this) { SelectedcustomerData = data });
        }


        //Add party selected event
        public static RoutedEvent AddPartySelectedEvent = EventManager.RegisterRoutedEvent("AddPartySelected",
             RoutingStrategy.Bubble, typeof(AddCustomerEventHandler), typeof(CustomerSuggestionTextBox));

        public event AddCustomerEventHandler AddPartySelected
        {
            add
            {
                AddHandler(AddPartySelectedEvent, value);
            }
            remove
            {
                RemoveHandler(AddPartySelectedEvent, value);
            }
        }


        protected virtual void OnAddPartySelected()
        {

            
            RaiseEvent(new AddCustomerEventArgs(AddPartySelectedEvent, this));
        }


        public void setcustomerData(customerModel data)
        {
            _customerData = data;
        }



        //public List<customerModel> CustomerSuggestions
        //{
        //    get { return (List<customerModel>)GetValue(SuggestionsProperty); }
        //    set { SetValue(SuggestionsProperty, value); }
        //}

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SuggestionsProperty =
        //    DependencyProperty.Register("CustomerSuggestions", typeof(ObservableCollection<string>), typeof(CustomerSuggestionTextBox), new PropertyMetadata(default));

        //public ObservableCollection<string> CustomerSuggestions
        //{
        //    get { return (ObservableCollection<string>)GetValue(SuggestionsProperty); }
        //    set { SetValue(SuggestionsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SuggestionsProperty =
        //    DependencyProperty.Register("CustomerSuggestions", typeof(ObservableCollection<string>), typeof(CustomerSuggestionTextBox), new PropertyMetadata(default));



        private void TextBoxPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Down )
            {
                suggestion.CustomerSuggestions.Focus();
                var temp = suggestion.CustomerSuggestions.IsFocused;

                var temp1 = suggestion.CustomerSuggestions.IsKeyboardFocused ; 

                suggestion.CustomerSuggestions.SelectedIndex = 0;
                
            }
        }

        private void TextBoxKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                suggestion.CustomerSuggestionPopup.IsOpen = false;
            }
        }
    }
}
