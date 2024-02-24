using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TAN.Controls
{
    [TemplatePart(Name = "PlaceHolderTextBox", Type = typeof(TextBlock))]
    public class CusTextBox : TextBox
    {



        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(CusTextBox), new PropertyMetadata(string.Empty));


        public string BalanceHolder
        {
            get { return (string)GetValue(BalanceHolderProperty); }
            set { SetValue(BalanceHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BalanceHolderProperty =
            DependencyProperty.Register("BalanceHolder", typeof(string), typeof(CusTextBox), new PropertyMetadata(string.Empty));

        public string SetText
        {
            get { return (string)GetValue(SetTextProperty); }
            set { SetValue(SetTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetTextProperty =
            DependencyProperty.Register("SetText", typeof(string), typeof(CusTextBox), new PropertyMetadata(string.Empty));

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyPropertyKey, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey IsEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly("IsEmpty", typeof(bool), typeof(CusTextBox), new PropertyMetadata(true));


        public static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;








        static CusTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CusTextBox), new FrameworkPropertyMetadata(typeof(CusTextBox)));
        }
        private TextBlock textBlock;
        public override void OnApplyTemplate()
        {

            textBlock = this.Template.FindName("PlaceHolderTextBox", this) as TextBlock;
            bool setTextbool = String.IsNullOrEmpty(SetText);
            if (!setTextbool)
            {
                this.Text = SetText;
                var sb = new Storyboard();
                var ta = new ThicknessAnimation();
                ta.BeginTime = new TimeSpan(0);
                ta.SetValue(Storyboard.TargetProperty, textBlock);
                Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

                ta.From = new Thickness(10, 15, 0, 0);
                ta.To = new Thickness(10, 0, 0, 0);
                ta.Duration = new Duration(TimeSpan.FromMilliseconds(180));

                sb.Children.Add(ta);
                sb.Begin(this);
            }
            base.OnApplyTemplate();
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            IsEmpty = String.IsNullOrEmpty(Text);
            base.OnTextChanged(e);
        }
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 45, 141, 236));
            textBlock.FontWeight = FontWeights.SemiBold;
            textBlock.FontSize = 14;
            if (IsEmpty)
            {







                var sb = new Storyboard();
                var ta = new ThicknessAnimation();
                ta.BeginTime = new TimeSpan(0);
                ta.SetValue(Storyboard.TargetProperty, textBlock);
                Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

                ta.From = new Thickness(10, 15, 0, 0);
                ta.To = new Thickness(10, 0, 0, 0);
                ta.Duration = new Duration(TimeSpan.FromMilliseconds(180));

                sb.Children.Add(ta);
                sb.Begin(this);



            }
            //IsEmpty1 = true;
            base.OnGotKeyboardFocus(e);
        }
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {

            if (IsEmpty)
            {

                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 169, 172, 180));

                textBlock.FontWeight = FontWeights.Regular;
                textBlock.FontSize = 16;


                var sb = new Storyboard();
                var ta = new ThicknessAnimation();
                ta.BeginTime = new TimeSpan(0);
                ta.SetValue(Storyboard.TargetProperty, textBlock);
                Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

                ta.From = new Thickness(10, 0, 0, 0);
                ta.To = new Thickness(10, 15, 0, 0);
                ta.Duration = new Duration(TimeSpan.FromMilliseconds(180));

                sb.Children.Add(ta);
                sb.Begin(this);
                base.OnLostKeyboardFocus(e);
            }
            else
            {
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 169, 172, 180));

                textBlock.FontWeight = FontWeights.Regular;
                textBlock.FontSize = 14;
            }
        }

    }
}
