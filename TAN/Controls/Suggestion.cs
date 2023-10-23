using System;
using System.Collections.ObjectModel;
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
    ///     <MyNamespace:Suggestion/>
    ///
    /// </summary>

    [TemplatePart(Name = "Suggestions", Type = typeof(ListBox))]
    [TemplatePart(Name = "SuggestionPopup", Type = typeof(Popup))]

    public class Suggestion : Control
    {
        static Suggestion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Suggestion), new FrameworkPropertyMetadata(typeof(Suggestion)));
        }
        public ListBox Suggestions;
        public Popup SuggestionPopup;
        TextBox _textbox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                Suggestions = this.Template.FindName("Suggestions", this) as ListBox;
                SuggestionPopup = this.Template.FindName("SuggestionPopup", this) as Popup;
                Suggestions.PreviewMouseDown += (s, e) =>
                {
                    SuggestionsPreviewMouseDown(e);
                };
                Suggestions.KeyDown += (s, e) =>
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
                    SuggestionPopup.IsOpen = false;
                }
            }
        }

        private void SuggestionsPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TextBlock textBlock = e.OriginalSource as TextBlock;
                if (textBlock != null)
                {
                    _textbox.Text = textBlock.Text;
                    SuggestionPopup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        internal void ShowSuggestion(ObservableCollection<string> listSuggestion, TextBox textBox)
        {

            _textbox = textBox;
            Suggestions.Items.Clear();



            if (listSuggestion != null)
                foreach (var each in listSuggestion)
                    Suggestions.Items.Add(each);



            SuggestionPopup.IsOpen = Suggestions.Items.Count > 0;
            Suggestions.Items.Filter = f =>
            {
                string _text = f as string;
                return _text.StartsWith(_textbox.Text, StringComparison.CurrentCultureIgnoreCase) &&
                !(string.Equals(_text, _textbox.Text, StringComparison.CurrentCultureIgnoreCase));
            };

        }

        internal void LostFocuss(RoutedEventArgs e)
        {
            Suggestions.Items.Clear();
            SuggestionPopup.IsOpen = false;
        }
    }
}
