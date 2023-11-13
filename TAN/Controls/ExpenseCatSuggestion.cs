using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
    [TemplatePart(Name = "ExpenseCatSuggestionsDataGrid", Type = typeof(ListBox))]
    [TemplatePart(Name = "ExpenseCatSuggestionPopup", Type = typeof(Popup))]

    public class ExpenseCatSuggestion : Control
    {
        static ExpenseCatSuggestion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpenseCatSuggestion), new FrameworkPropertyMetadata(typeof(ExpenseCatSuggestion)));
        }
        public DataGrid _expenseCatSuggestions;
        public Popup _expenseCatSuggestionPopup;
        TextBox _textbox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                _expenseCatSuggestions = this.Template.FindName("ExpenseCatSuggestionsDataGrid", this) as DataGrid;
                _expenseCatSuggestionPopup = this.Template.FindName("ExpenseCatSuggestionPopup", this) as Popup;

                BindingOperations.EnableCollectionSynchronization(ExpenseCatMainData, _lockMutex);
                _expenseCatSuggestions.ItemsSource = ExpenseCatMainData;
                







                _expenseCatSuggestions.GotFocus += (s, e) =>
                {
                    var temp = 1;
                };
                _expenseCatSuggestions.GotKeyboardFocus += (s, e) =>
                {
                    var temp = 2;
                };
                _expenseCatSuggestions.PreviewMouseDown += (s, e) =>
                {
                    SuggestionsPreviewMouseDown(e);
                };
                _expenseCatSuggestions.KeyDown += (s, e) =>
                {
                    SuggestionskeyDown(e);
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
                    _expenseCatSuggestionPopup.IsOpen = false;
                }
            }
        }
        bool _needToFetchData = true;
        private void SuggestionsPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {


                ExpenseCategoryModel temp = (ExpenseCategoryModel)_expenseCatSuggestions.SelectedItem;
                if (temp != null)
                {
                    if (temp.ExpenseCategaryID == -20)
                    {
                        _onAddExpesneCat1();
                    }
                    else
                    {
                        _textbox.Text = temp.ExpenseCategaryName;
                        _onexpenseCatSeleted(temp);
                        _needToFetchData = true;
                    }
                    _expenseCatSuggestionPopup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

       

        public delegate void OnExpenseCatSeleted1(ExpenseCategoryModel data);
        public delegate void OnAddExpesneCat1();

        private OnExpenseCatSeleted1 _onexpenseCatSeleted;
        private OnAddExpesneCat1 _onAddExpesneCat1;


        public void AssginFunction(OnExpenseCatSeleted1 data , OnAddExpesneCat1 data1)
        {
            _onexpenseCatSeleted = data;
            _onAddExpesneCat1 = data1;
        }


        
        public void AssginParties(TextBox searchTextBox)
        {
            _textbox = searchTextBox;
            _expenseCatSuggestionPopup.IsOpen = true;

            string searchKey = searchTextBox.Text.ToLower();
            if(_needToFetchData)
            {
                List<ExpenseCategoryModel> MainData1 = ExpenseCategorySqllite.readAll();
                mainData = MainData1;
            }
            if (searchKey == "")
            {
                
                

                _ = LoadDataAsync();
                
                    DataGridRow row = (DataGridRow)_expenseCatSuggestions.ItemContainerGenerator.ContainerFromIndex(0);
                    if (row != null)
                    {
                        SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, 255, 104, 0));
                        row.Background = brush;
                    }
                
            }

            else
            {
                _ = SearchDataAsync(searchKey);
                DataGridRow row = (DataGridRow)_expenseCatSuggestions.ItemContainerGenerator.ContainerFromIndex(0);
                if (row != null)
                {
                    SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, 255, 104, 0));
                    row.Background = brush;
                }
            }
                 

        }
        private List<ExpenseCategoryModel> mainData;
        private ObservableCollection<ExpenseCategoryModel> _expenseCatMainData;
        public ObservableCollection<ExpenseCategoryModel> ExpenseCatMainData
        {
            get { return _expenseCatMainData; }
            set { _expenseCatMainData = value; }
        }
        private object _lockMutex = new object();
        public void AssginExpenseMainData(List<ExpenseCategoryModel> paraData)
        {
            _expenseCatMainData = new ObservableCollection<ExpenseCategoryModel>();

            mainData = paraData;
            
        
        }
        
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                _expenseCatMainData.Clear();
                _expenseCatMainData.Add(new ExpenseCategoryModel(-20, "Add Expense Category"));

                
                
                foreach (ExpenseCategoryModel c in mainData.Take(20))
                {
                    _expenseCatMainData.Add(c);
                }
                
            });
        }
        private Task SearchDataAsync(string key)
        {
            return Task.Factory.StartNew(() =>
            {
                _expenseCatMainData.Clear();
                _expenseCatMainData.Add(new ExpenseCategoryModel(-20, "Add Expense Category"));
                foreach (ExpenseCategoryModel c in mainData.Where(s => s.ExpenseCategaryName.ToLower().Contains(key)).ToList())
                {
                    _expenseCatMainData.Add(c);
                }
            });
        }

        internal void LostFocuss(RoutedEventArgs e)
        {

            _expenseCatSuggestionPopup.IsOpen = false;
        }
    }
}
