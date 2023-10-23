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
    ///     <MyNamespace:ProductSuggestionTextBox/>
    ///
    /// </summary>


    [TemplatePart(Name = "ProductSuggestionEditor", Type = typeof(TextBox))]


    public class ProductSuggestionTextBox : Control
    {

        public delegate void ProductChangeEventHandler(object sender, ProductDataEventArgs args);
        static ProductSuggestionTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProductSuggestionTextBox), new FrameworkPropertyMetadata(typeof(ProductSuggestionTextBox)));
        }
        TextBox textBox;
        ProductSuggestion suggestion;

        public productVersionModel ProductData { get; set; }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("ProductSuggestionEditor", this) as TextBox;
                suggestion = this.Template.FindName("ProductSuggestion", this) as ProductSuggestion;


                if (textBox != null)
                {
                    ProductMainData = ProductVersionModelSqlite.readAll();
                    suggestion.AssginProductMainData(ProductMainData);
                    suggestion.AssginFunction(OnProductSeleted);
                    textBox.PreviewKeyDown += (s, e) => { TextBoxPreviewKeyDown(e); };
                    textBox.KeyDown += (s, e) => { TextBoxKeyDown(e); };
                    textBox.TextChanged += (s, e) => { suggestion.AssginParties(textBox); };
                    //textBox.LostFocus += (s, e) => { suggestion.LostFocuss(e); };

                }

            }
        }




        public List<productVersionModel> ProductMainData
        {
            get { return (List<productVersionModel>)GetValue(ProductDataProperty); }
            set { SetValue(ProductDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductDataProperty =
            DependencyProperty.Register("ProductMainData", typeof(List<productVersionModel>), typeof(ProductSuggestionTextBox), new PropertyMetadata(null));




        public string Text1
        {
            get { return (string)GetValue(Text1Property); }
            set { SetValue(Text1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Text1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Text1Property =
            DependencyProperty.Register("Text1", typeof(string), typeof(ProductSuggestionTextBox), new PropertyMetadata(string.Empty));


        public static RoutedEvent ProductSelectedEvent = EventManager.RegisterRoutedEvent("ProductSeleted",
            RoutingStrategy.Bubble, typeof(ProductChangeEventHandler), typeof(ProductSuggestionTextBox));

        public event ProductChangeEventHandler ProductSeleted
        {
            add
            {
                AddHandler(ProductSelectedEvent, value);
            }
            remove
            {
                RemoveHandler(ProductSelectedEvent, value);
            }
        }


        protected virtual void OnProductSeleted(productVersionModel data)
        {

            setProductData(data);
            RaiseEvent(new ProductDataEventArgs(ProductSelectedEvent, this) { SelectedProductData = data });
        }


        public void setProductData(productVersionModel data)
        {
            ProductData = data;
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
            if (e.Key == Key.Down)
            {
                suggestion.ProductSuggestions.Focus();
                suggestion.ProductSuggestions.SelectedIndex = 0;
                e.Handled = true;
            }
        }

        private void TextBoxKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                suggestion.ProductSuggestionPopup.IsOpen = false;
            }
        }
    }
}
