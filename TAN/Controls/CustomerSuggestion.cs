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
    //[TemplatePart(Name = "CustomerSuggestions", Type = typeof(ListBox))]
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

                BindingOperations.EnableCollectionSynchronization(CustomerMainData, _lockMutex);
                CustomerSuggestions.ItemsSource = CustomerMainData;
                //CustomerSuggestions.Rows[0].DefaultCellStyle.forecolor = "Color.Red";
                


                



                
                CustomerSuggestions.PreviewMouseDown += (s, e) =>
                {
                    SuggestionsPreviewMouseDown(e);
                };
                CustomerSuggestions.SelectionChanged += (s, e) =>
                {
                    SuggestionskeyDown();
                };


            }
        }

        private void SuggestionskeyDown()
        {
            //if (e.Key == Key.Enter)
            //{
            //    CustomerSuggestionPopup.IsOpen = false;
            //}

            customerModel data = (customerModel)CustomerSuggestions.SelectedItem  ;
            if(data!=null)
            {
                if(data.customerName!=null)
                {
                    //if(data.customerID!=-20)
                        _textbox.Text = data.customerName;
                }
            }
            
                    
        }
        bool _needToFetchData = true;
        private void SuggestionsPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                

                customerModel temp = (customerModel)CustomerSuggestions.SelectedItem;
                if (temp != null)
                {
                    if (temp.customerID == -20)
                    {
                        _onAddPartySelected1();
                    }
                    else
                    {
                        _textbox.Text = temp.customerName;
                        _onCustomerSeleted(temp);
                        _needToFetchData = true;
                    }
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

        public delegate void OnCustomerSeleted1(customerModel data);
        public delegate void OnAddPartySelected1();

        private OnCustomerSeleted1 _onCustomerSeleted;
        private OnAddPartySelected1 _onAddPartySelected1;


        public void AssginFunction(OnCustomerSeleted1 data , OnAddPartySelected1 data1, TextBox searchTextBox)
        {
            _onCustomerSeleted = data;
            _onAddPartySelected1 = data1;
            _textbox = searchTextBox;
        }

        public void TextBoxKeyDown(object sender , KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                CustomerSuggestionPopup.IsOpen = false;
            }
            else if (e.Key == Key.Down)
            {
                if (CustomerSuggestionPopup.IsOpen)
                {

                    if (CustomerSuggestions.SelectedIndex < CustomerSuggestions.Items.Count - 1)
                    {
                        CustomerSuggestions.SelectedIndex += 1;
                    }
                    else
                    {
                        CustomerSuggestions.SelectedIndex = 0;
                    }
                    CustomerSuggestions.ScrollIntoView(CustomerSuggestions.SelectedItem);

                }
                

            }
            else if (e.Key == Key.Up)
            {
                if (CustomerSuggestionPopup.IsOpen)
                {

                    if (CustomerSuggestions.SelectedIndex != 0)
                        CustomerSuggestions.SelectedIndex -= 1;
                    else
                        CustomerSuggestions.SelectedIndex = CustomerSuggestions.Items.Count - 1;
                    CustomerSuggestions.ScrollIntoView(CustomerSuggestions.SelectedItem);


                }
            }
            else
            {
                string searchKey = _textbox.Text.ToLower();
                if (e.Key == Key.Back)
                {
                    if (searchKey.Length > 1)
                        searchKey = searchKey.Substring(0, searchKey.Length - 1);
                    else
                        searchKey = "";
                }
                


                if (!CustomerSuggestionPopup.IsOpen)
                    CustomerSuggestionPopup.IsOpen = true;

                
                if (_needToFetchData)
                {
                    List<customerModel> CustomerMainData = CustomerSqllite.readAll();
                    mainData = CustomerMainData;
                }
                if (searchKey == "")
                {



                    _ = LoadDataAsync();

                    DataGridRow row = (DataGridRow)CustomerSuggestions.ItemContainerGenerator.ContainerFromIndex(0);
                    if (row != null)
                    {
                        SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, 255, 104, 0));
                        row.Background = brush;
                    }

                }

                else
                {
                    _ = SearchDataAsync(searchKey);
                    DataGridRow row = (DataGridRow)CustomerSuggestions.ItemContainerGenerator.ContainerFromIndex(0);
                    if (row != null)
                    {
                        SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, 255, 104, 0));
                        row.Background = brush;
                    }
                }
            }
        }

        //private List<customerModel> _products;
        public void AssginParties(TextBox searchTextBox)
        {
            _textbox = searchTextBox;
            if(!CustomerSuggestionPopup.IsOpen)
                CustomerSuggestionPopup.IsOpen = true;
            
            string searchKey = searchTextBox.Text.ToLower();
            if(_needToFetchData)
            {
                List<customerModel> CustomerMainData = CustomerSqllite.readAll();
                mainData = CustomerMainData;
            }
            if (searchKey == "")
            {
                
                

                _ = LoadDataAsync();
                
                    DataGridRow row = (DataGridRow)CustomerSuggestions.ItemContainerGenerator.ContainerFromIndex(0);
                    if (row != null)
                    {
                        SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, 255, 104, 0));
                        row.Background = brush;
                    }
                
            }

            else
            {
                _ = SearchDataAsync(searchKey);
                DataGridRow row = (DataGridRow)CustomerSuggestions.ItemContainerGenerator.ContainerFromIndex(0);
                if (row != null)
                {
                    SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, 255, 104, 0));
                    row.Background = brush;
                }
            }
                 

        }
        private List<customerModel> mainData;
        private ObservableCollection<customerModel> _customerMainData;
        public ObservableCollection<customerModel> CustomerMainData
        {
            get { return _customerMainData; }
            set { _customerMainData = value; }
        }
        private object _lockMutex = new object();
        public void AssginCustomerMainData(List<customerModel> paraData)
        {
            _customerMainData = new ObservableCollection<customerModel>();

            mainData = paraData;
            
        //_customerMainData = productMainData;
        }
        
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                _customerMainData.Clear();
                _customerMainData.Add(new customerModel(-20, "Add Party", 1, "", "", 0, 0));

                
                
                foreach (customerModel c in mainData.Take(20))
                {
                    _customerMainData.Add(c);
                }
                
            });
        }
        private Task SearchDataAsync(string key)
        {
            return Task.Factory.StartNew(() =>
            {
                _customerMainData.Clear();
                _customerMainData.Add(new customerModel(-20, "Add Party", 1, "", "", 0, 0));
                foreach (customerModel c in mainData.Where(s => s.customerName.ToLower().Contains(key)).ToList())
                {
                    _customerMainData.Add(c);
                }
            });
        }

        internal void LostFocuss(RoutedEventArgs e)
        {
            
            CustomerSuggestionPopup.IsOpen = false;
        }
    }
}
