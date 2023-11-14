using DataBaseManger.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
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
    //[TemplatePart(Name = "ProductSuggestions", Type = typeof(ListBox))]
    [TemplatePart(Name = "ExpenseItemSuggestionPopup1", Type = typeof(Popup))]

    public class ExpenseItemSuggestion : Control
    {
        static ExpenseItemSuggestion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpenseItemSuggestion), new FrameworkPropertyMetadata(typeof(ExpenseItemSuggestion)));
        }
        public DataGrid ExpenseItemSuggestionsDataGrid;
        public Popup ExpenseItemSuggestionPopup;
        TextBox _textbox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                ExpenseItemSuggestionsDataGrid = this.Template.FindName("ExpenseItemSuggestionsDataGrid", this) as DataGrid;
                ExpenseItemSuggestionPopup = this.Template.FindName("ExpenseItemSuggestionPopup1", this) as Popup;

                BindingOperations.EnableCollectionSynchronization(ExpenseItemMainData, _lockMutex);
                ExpenseItemSuggestionsDataGrid.ItemsSource = ExpenseItemMainData;

                ExpenseItemSuggestionsDataGrid.PreviewMouseDown += (s, e) =>
                {
                    SuggestionsPreviewMouseDown(e);
                };

                ExpenseItemSuggestionsDataGrid.KeyDown += (s, e) =>
                {
                    SuggestionskeyDown(e);
                };

            }
        }
        private void SuggestionskeyDown(KeyEventArgs e)
        {
            ExpenseItemModel temp = (ExpenseItemModel)ExpenseItemSuggestionsDataGrid.SelectedItem;
            if (temp != null)
            {
                _textbox.Text = temp.ExpenseItemName;

                if (e.Key == Key.Enter)
                {
                    ExpenseItemSuggestionPopup.IsOpen = false;
                }
            }
        }

        private void SuggestionsPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ExpenseItemModel data = (ExpenseItemModel)ExpenseItemSuggestionsDataGrid.SelectedItem;
                if (data != null)
                {
                    
                    
                    e.Handled = true;
                    if (data.ExpenseItemID == -20)
                    {
                        _onAddItem();
                    }
                    else
                    {
                        _textbox.Text = data.ExpenseItemName;
                        _onExpenseItemDataSelected(data);
                        
                    }
                    ExpenseItemSuggestionPopup.IsOpen = false;
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
        
        public delegate void OnExpenseItemDataSelected(ExpenseItemModel data);
        public delegate void OnAddItem();

        private OnExpenseItemDataSelected _onExpenseItemDataSelected;
        private OnAddItem _onAddItem;


        public void AssginFunction(OnExpenseItemDataSelected temp , OnAddItem temp2 )
        {
            _onExpenseItemDataSelected = temp;
            _onAddItem = temp2;
        }

        //private List<productVersionModel> _products;
        public void AssginParties(TextBox searchTextBox)
        {
            _textbox = searchTextBox;
            ExpenseItemSuggestionPopup.IsOpen = true;
            string searchKey = searchTextBox.Text.ToLower();
            if (searchKey == "")
            {
                _ = LoadDataAsync();

            }
            else
            {
                _ = SearchDataAsync(searchKey);
            }
        }
        


        private List<ExpenseItemModel> mainData;
        private ObservableCollection<ExpenseItemModel> _expenseItemMainData;
        public ObservableCollection<ExpenseItemModel> ExpenseItemMainData
        {
            get { return _expenseItemMainData; }
            set { _expenseItemMainData = value; }
        }
        private object _lockMutex = new object();
        public void AssginProductMainData(List<ExpenseItemModel> paraData)
        {
            _expenseItemMainData = new ObservableCollection<ExpenseItemModel>();

            mainData = paraData;

            
        }

        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                _expenseItemMainData.Clear();
                
                _expenseItemMainData.Add(new ExpenseItemModel(-20, "Add Item", 1));




                foreach (ExpenseItemModel c in mainData.Take(20))
                {
                    _expenseItemMainData.Add(c);
                }

            });
        }
        private Task SearchDataAsync(string key)
        {
            return Task.Factory.StartNew(() =>
            {
                _expenseItemMainData.Clear();
                _expenseItemMainData.Add(new ExpenseItemModel(-20, "Add Item", 1));

                foreach (ExpenseItemModel c in mainData.Where(s => s.ExpenseItemName.ToLower().Contains(key)).ToList())
                {
                    _expenseItemMainData.Add(c);
                }
            });
        }




        internal void LostFocuss(RoutedEventArgs e)
        {

            ExpenseItemSuggestionPopup.IsOpen = false;
        }
    }
}
