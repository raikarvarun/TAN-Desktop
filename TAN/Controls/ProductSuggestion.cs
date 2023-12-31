﻿using DataBaseManger.Model;
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
                ProductSuggestions = this.Template.FindName("ProductSuggestionsDataGrid", this) as DataGrid;
                ProductSuggestionPopup = this.Template.FindName("ProductSuggestionPopup", this) as Popup;

                BindingOperations.EnableCollectionSynchronization(ProductMainData, _lockMutex);
                ProductSuggestions.ItemsSource = ProductMainData;

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
                productVersionModel data = (productVersionModel)ProductSuggestions.SelectedItem;
                if (data != null)
                {
                    
                    
                    e.Handled = true;
                    if (data.productNo == -20)
                    {
                        _onAddItem();
                    }
                    else
                    {
                        _textbox.Text = data.productName;
                        _onProductDataSelected(data);
                        
                    }
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
        
        public delegate void OnProductDataSelected(productVersionModel data);
        public delegate void OnAddItem();

        private OnProductDataSelected _onProductDataSelected;
        private OnAddItem _onAddItem;


        public void AssginFunction(OnProductDataSelected temp , OnAddItem temp2 )
        {
            _onProductDataSelected = temp;
            _onAddItem = temp2;
        }

        //private List<productVersionModel> _products;
        public void AssginParties(TextBox searchTextBox)
        {
            _textbox = searchTextBox;
            ProductSuggestionPopup.IsOpen = true;
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
        


        private List<productVersionModel> mainData;
        private ObservableCollection<productVersionModel> _productMainData;
        public ObservableCollection<productVersionModel> ProductMainData
        {
            get { return _productMainData; }
            set { _productMainData = value; }
        }
        private object _lockMutex = new object();
        public void AssginProductMainData(List<productVersionModel> paraData)
        {
            _productMainData = new ObservableCollection<productVersionModel>();

            mainData = paraData;

            
        }

        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                _productMainData.Clear();
                _productMainData.Add(new productVersionModel(-20, "", 1, 0, "", "Add Product", 0, 0,0,0));



                foreach (productVersionModel c in mainData.Take(20))
                {
                    _productMainData.Add(c);
                }

            });
        }
        private Task SearchDataAsync(string key)
        {
            return Task.Factory.StartNew(() =>
            {
                _productMainData.Clear();
                _productMainData.Add(new productVersionModel(-20, "", 1, 0,  "", "Add Product", 0, 0,0,0));

                foreach (productVersionModel c in mainData.Where(s => s.productName.ToLower().Contains(key)).ToList())
                {
                    _productMainData.Add(c);
                }
            });
        }




        internal void LostFocuss(RoutedEventArgs e)
        {

            ProductSuggestionPopup.IsOpen = false;
        }
    }
}
