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
    [TemplatePart(Name = "ExpenseCatSuggestionEditor", Type = typeof(TextBox))]
    public class ExpenseCatSuggestionTextBox : Control
    {
        public delegate void ExpenseCatChangeEventHandler(object sender, ExpenseCatDataEventArgs args);
        public delegate void AddExpenseCatEventHandler(object sender, AddExpenseCatEventArgs args);

        private ExpenseCategoryModel _expenseCategory { get; set; }

        static ExpenseCatSuggestionTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpenseCatSuggestionTextBox), new FrameworkPropertyMetadata(typeof(ExpenseCatSuggestionTextBox)));
        }
        TextBox textBox;
        ExpenseCatSuggestion suggestion;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("ExpenseCatSuggestionEditor", this) as TextBox;
                suggestion = this.Template.FindName("ExpenseCatSuggestion", this) as ExpenseCatSuggestion;
                if (textBox != null)
                {
                    List<ExpenseCategoryModel> ExpenseCatMainData = ExpenseCategorySqllite.readAll();
                    suggestion.AssginExpenseMainData(ExpenseCatMainData);
                    suggestion.AssginFunction(OnExpenseCatSeleted, OnAddExpenseCat);
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
            if (!suggestion._expenseCatSuggestionPopup.IsOpen) 
            {
                suggestion._expenseCatSuggestionPopup.IsOpen = true;
                suggestion.AssginParties(textBox);
            }
                

        }


        //Expense Cat selected event
        public static RoutedEvent ExpenseCatSelectedEvent = EventManager.RegisterRoutedEvent("ExpenseCatSeleted",
             RoutingStrategy.Bubble, typeof(ExpenseCatChangeEventHandler), typeof(ExpenseCatSuggestionTextBox));

        public event ExpenseCatChangeEventHandler ExpenseCatSeleted
        {
            add
            {
                AddHandler(ExpenseCatSelectedEvent, value);
            }
            remove
            {
                RemoveHandler(ExpenseCatSelectedEvent, value);
            }
        }


        protected virtual void OnExpenseCatSeleted(ExpenseCategoryModel data)
        {

            setData(data);
            RaiseEvent(new ExpenseCatDataEventArgs(ExpenseCatSelectedEvent, this) { SelectedData = data });
        }


        //Add party selected event
        public static RoutedEvent AddExpenseCatEvent = EventManager.RegisterRoutedEvent("AddExpenseCat",
             RoutingStrategy.Bubble, typeof(AddExpenseCatEventHandler), typeof(ExpenseCatSuggestionTextBox));

        public event AddExpenseCatEventHandler AddExpenseCat
        {
            add
            {
                AddHandler(AddExpenseCatEvent, value);
            }
            remove
            {
                RemoveHandler(AddExpenseCatEvent, value);
            }
        }


        protected virtual void OnAddExpenseCat()
        {

            
            RaiseEvent(new AddExpenseCatEventArgs(AddExpenseCatEvent, this));
        }


        public void setData(ExpenseCategoryModel data)
        {
            _expenseCategory = data;
        }



        


        private void TextBoxPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Down )
            {
                suggestion._expenseCatSuggestions.Focus();
                

                suggestion._expenseCatSuggestions.SelectedIndex = 0;
                
            }
        }

        private void TextBoxKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                suggestion._expenseCatSuggestionPopup.IsOpen = false;
            }
        }
    }
}
