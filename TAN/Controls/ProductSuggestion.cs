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
    ///     <MyNamespace:ProductSuggestion/>
    ///
    /// </summary>
    [TemplatePart(Name = "ProductSuggestions", Type = typeof(ListBox))]
    [TemplatePart(Name = "ProductSuggestionPopup", Type = typeof(Popup))]

    public class ProductSuggestion : Control
    {
        static ProductSuggestion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProductSuggestion), new FrameworkPropertyMetadata(typeof(ProductSuggestion)));
        }
        public DataGrid ProductSuggestions;
        public Popup ProductSuggestionPopup;
        TextBox _textbox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                ProductSuggestions = this.Template.FindName("ProductSuggestions", this) as DataGrid;
                ProductSuggestionPopup = this.Template.FindName("ProductSuggestionPopup", this) as Popup;



                ProductSuggestions.PreviewMouseDown += (s, e) =>
                {
                    SuggestionsPreviewMouseDown(e);
                };

                ProductSuggestions.KeyDown += (s, e) =>
                {
                    SuggestionskeyDown(e);
                };

            }
        }
        private void SuggestionskeyDown(KeyEventArgs e)
        {
            productVersionModel temp = (productVersionModel)ProductSuggestions.SelectedItem;
            if (temp != null)
            {
                _textbox.Text = temp.productName;

                if (e.Key == Key.Enter)
                {
                    ProductSuggestionPopup.IsOpen = false;
                }
            }
        }

        private void SuggestionsPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                productVersionModel temp = (productVersionModel)ProductSuggestions.SelectedItem;
                if (temp != null)
                {
                    _textbox.Text = temp.productName;
                    onSlelevte(temp);
                    ProductSuggestionPopup.IsOpen = false;
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
        public delegate void TEmpfun(productVersionModel data);

        private TEmpfun onSlelevte;
        public void AssginFunction(TEmpfun temp)
        {
            onSlelevte = temp;
        }

        //private List<productVersionModel> _products;
        public void AssginParties(TextBox searchTextBox)
        {
            _textbox = searchTextBox;
            ProductSuggestionPopup.IsOpen = true;
            string searchKey = searchTextBox.Text.ToLower();
            if (searchKey == "")

                ProductSuggestions.ItemsSource = _productMainData.Take(40);


            else
                ProductSuggestions.ItemsSource = _productMainData.Where(s => s.productName.ToLower().Contains(searchKey)).ToList().Take(30);
        }
        private List<productVersionModel> _productMainData;
        public void AssginProductMainData(List<productVersionModel> productMainData)
        {
            _productMainData = productMainData;
        }
        internal void LostFocuss(RoutedEventArgs e)
        {

            ProductSuggestionPopup.IsOpen = false;
        }
    }
}
