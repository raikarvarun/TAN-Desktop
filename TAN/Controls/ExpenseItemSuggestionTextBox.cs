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


    [TemplatePart(Name = "ExpenseCatSuggestionEditor", Type = typeof(TextBox))]
    



    public class ExpenseItemSuggestionTextBox : Control
    {

        public delegate void ExpenseChangeEventHandler(object sender, ExpenseItemDataEventArgs args);
        public delegate void AddExpenseItemEventHandler(object sender, AddProductrEventArgs args);

        static ExpenseItemSuggestionTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpenseItemSuggestionTextBox), new FrameworkPropertyMetadata(typeof(ExpenseItemSuggestionTextBox)));
        }
        TextBox textBox;
        ExpenseItemSuggestion suggestion;

        public ExpenseItemModel ExpenseItemData { get; set; }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("ExpenseCatSuggestionEditor", this) as TextBox;
                suggestion = this.Template.FindName("ExpenseItemSuggestion", this) as ExpenseItemSuggestion;


                if (textBox != null)
                {
                    ExpenseItemMainData = ExpenseItemSqllite.readAll();
                    suggestion.AssginProductMainData(ExpenseItemMainData);
                    suggestion.AssginFunction(OnExpenseItemSeleted, OnAddItem);
                    textBox.PreviewKeyDown += (s, e) => { TextBoxPreviewKeyDown(e); };
                    textBox.KeyDown += (s, e) => { TextBoxKeyDown(e); };
                    textBox.TextChanged += (s, e) => { suggestion.AssginParties(textBox); };
                    textBox.MouseDown += (s, e) => { TextBoxMouseDown(); };
                    //textBox.LostFocus += (s, e) => { suggestion.LostFocuss(e); };

                }

            }
        }




        public List<ExpenseItemModel> ExpenseItemMainData
        {
            get { return (List<ExpenseItemModel>)GetValue(ProductDataProperty); }
            set { SetValue(ProductDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductDataProperty =
            DependencyProperty.Register("ExpenseItemMainData", typeof(List<ExpenseItemModel>), typeof(ExpenseItemSuggestionTextBox), new PropertyMetadata(null));




        public string Text1
        {
            get { return (string)GetValue(Text1Property); }
            set { SetValue(Text1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Text1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Text1Property =
            DependencyProperty.Register("Text1", typeof(string), typeof(ExpenseItemSuggestionTextBox), new PropertyMetadata(string.Empty));


        public static RoutedEvent ExpenseItemSelectedEvent = EventManager.RegisterRoutedEvent("ExpenseItemSeleted",
            RoutingStrategy.Bubble, typeof(ExpenseChangeEventHandler), typeof(ExpenseItemSuggestionTextBox));

        public event ExpenseChangeEventHandler ExpenseItemSeleted
        {
            add
            {
                AddHandler(ExpenseItemSelectedEvent, value);
            }
            remove
            {
                RemoveHandler(ExpenseItemSelectedEvent, value);
            }
        }


        protected virtual void OnExpenseItemSeleted(ExpenseItemModel data)
        {

            setProductData(data);
            RaiseEvent(new ExpenseItemDataEventArgs(ExpenseItemSelectedEvent, this) { SelectedData = data });
        }


        //Add party selected event
        public static RoutedEvent AddExpenseItemEvent = EventManager.RegisterRoutedEvent("AddExpenseItem",
             RoutingStrategy.Bubble, typeof(AddExpenseItemEventHandler), typeof(ExpenseItemSuggestionTextBox));

        public event AddExpenseItemEventHandler AddExpenseItem
        {
            add
            {
                AddHandler(AddExpenseItemEvent, value);
            }
            remove
            {
                RemoveHandler(AddExpenseItemEvent, value);
            }
        }


        protected virtual void OnAddItem()
        {


            RaiseEvent(new AddProductrEventArgs(AddExpenseItemEvent, this));
        }


        public void setProductData(ExpenseItemModel data)
        {
            ExpenseItemData = data;
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
                suggestion.ExpenseItemSuggestionsDataGrid.Focus();
                suggestion.ExpenseItemSuggestionsDataGrid.SelectedIndex = 0;
                e.Handled = true;
            }
        }

        private void TextBoxKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                suggestion.ExpenseItemSuggestionPopup.IsOpen = false;
            }
        }
        private void TextBoxMouseDown()
        {
            if(!suggestion.ExpenseItemSuggestionPopup.IsOpen)
                suggestion.ExpenseItemSuggestionPopup.IsOpen = true;
            
        }
    }
}
