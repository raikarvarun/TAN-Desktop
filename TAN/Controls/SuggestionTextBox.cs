using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
    ///     <MyNamespace:SuggestionTextBox/>
    ///
    /// </summary>

    [TemplatePart(Name = "SuggestionEditor", Type = typeof(TextBox))]
    public class SuggestionTextBox : Control
    {
        static SuggestionTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SuggestionTextBox), new FrameworkPropertyMetadata(typeof(SuggestionTextBox)));
        }
        TextBox textBox;
        Suggestion suggestion;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("SuggestionEditor", this) as TextBox;
                suggestion = this.Template.FindName("Suggestion", this) as Suggestion;
                if (textBox != null)
                {
                    textBox.PreviewKeyDown += (s, e) => { TextBoxPreviewKeyDown(e); };
                    textBox.KeyDown += (s, e) => { TextBoxKeyDown(e); };
                    textBox.TextChanged += (s, e) => { suggestion.ShowSuggestion(Suggestions, textBox); };
                    textBox.LostFocus += (s, e) => { suggestion.LostFocuss(e); };

                }
            }
        }



        public ObservableCollection<string> Suggestions
        {
            get { return (ObservableCollection<string>)GetValue(SuggestionsProperty); }
            set { SetValue(SuggestionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuggestionsProperty =
            DependencyProperty.Register("Suggestions", typeof(ObservableCollection<string>), typeof(SuggestionTextBox), new PropertyMetadata(default));



        private void TextBoxPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Down && suggestion.Suggestions.Items.Count > 0 && !(e.OriginalSource is ListBoxItem))
            {
                suggestion.Suggestions.Focus();
                suggestion.Suggestions.SelectedIndex = 0;
                ListBoxItem listBoxItem =
                    suggestion.Suggestions.ItemContainerGenerator.ContainerFromItem(suggestion.Suggestions.SelectedIndex)
                    as ListBoxItem;
                listBoxItem.Focus();
                e.Handled = true;
            }

        }

        private void TextBoxKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                suggestion.SuggestionPopup.IsOpen = false;
            }
        }
    }
}
