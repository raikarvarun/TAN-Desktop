using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TAN.Helpers;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        public DashBoardView()
        {
            InitializeComponent();
            PrevButton = HomeButton;
            PrevButton.BorderBrush = Brushes.Red;
            PrevButton.Background = new SolidColorBrush(Color.FromArgb(255, 25, 31, 38));
            PrevButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            PrevButton.FontWeight = FontWeights.Medium;
        }

        private void DashBoardMainGrid_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
        Button PrevButton;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PrevButton.BorderBrush = Brushes.Transparent;
            PrevButton.Background = Brushes.Transparent;
            PrevButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
            PrevButton.FontWeight = FontWeights.Regular;
            if (PrevExpander != null)
            {
                PrevExpander.Background = Brushes.Transparent;
                PrevExpander.BorderBrush = Brushes.Transparent;

                PrevExpander.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
                PrevExpander.FontWeight = FontWeights.Regular;
            }


            Button currButton = sender as Button;
            currButton.BorderBrush = Brushes.Red;
            currButton.Background = new SolidColorBrush(Color.FromArgb(255, 25, 31, 38));
            currButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            currButton.FontWeight = FontWeights.Medium;


            PrevButton = currButton;


        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            ExpanderControl.Background = new SolidColorBrush(Color.FromArgb(255, 25, 31, 38));
            ExpanderControl.BorderBrush = Brushes.Red;
            ExpanderControl.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ExpanderControl.FontWeight = FontWeights.Medium;

            ExpanderControl1.Background = Brushes.Transparent;
            ExpanderControl1.BorderBrush = Brushes.Transparent;
            ExpanderControl1.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
            ExpanderControl1.FontWeight = FontWeights.Regular;

            PrevButton.BorderBrush = Brushes.Transparent;
            PrevButton.Background = Brushes.Transparent;
            PrevButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));

            Button currButton = sender as Button;
            currButton.Background = new SolidColorBrush(Color.FromArgb(255, 44, 54, 67));
            currButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            PrevButton = currButton;


        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            ExpanderControl1.Background = new SolidColorBrush(Color.FromArgb(255, 25, 31, 38));
            ExpanderControl1.BorderBrush = Brushes.Red;
            ExpanderControl1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ExpanderControl1.FontWeight = FontWeights.Medium;

            ExpanderControl.Background = Brushes.Transparent;
            ExpanderControl.BorderBrush = Brushes.Transparent;
            ExpanderControl.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
            ExpanderControl.FontWeight = FontWeights.Regular;

            PrevButton.BorderBrush = Brushes.Transparent;
            PrevButton.Background = Brushes.Transparent;
            PrevButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));

            Button currButton = sender as Button;
            currButton.Background = new SolidColorBrush(Color.FromArgb(255, 44, 54, 67));
            currButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            PrevButton = currButton;


        }
        private Expander PrevExpander;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrevButton.BorderBrush = Brushes.Transparent;
            PrevButton.Background = Brushes.Transparent;
            PrevButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
            PrevButton.FontWeight = FontWeights.Regular;

            if (PrevExpander != null)
            {
                PrevExpander.Background = Brushes.Transparent;
                PrevExpander.BorderBrush = Brushes.Transparent;

                PrevExpander.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
                PrevExpander.FontWeight = FontWeights.Regular;
            }

            ExpanderControl.Background = new SolidColorBrush(Color.FromArgb(255, 25, 31, 38));
            ExpanderControl.BorderBrush = Brushes.Red;

            ExpanderControl.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ExpanderControl.FontWeight = FontWeights.Medium;

            ExpanderControl.IsExpanded = !ExpanderControl.IsExpanded;




            PrevExpander = ExpanderControl;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PrevButton.BorderBrush = Brushes.Transparent;
            PrevButton.Background = Brushes.Transparent;
            PrevButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
            PrevButton.FontWeight = FontWeights.Regular;
            if (PrevExpander != null)
            {
                PrevExpander.Background = Brushes.Transparent;
                PrevExpander.BorderBrush = Brushes.Transparent;

                PrevExpander.Foreground = new SolidColorBrush(Color.FromArgb(255, 176, 179, 183));
                PrevExpander.FontWeight = FontWeights.Regular;
            }

            ExpanderControl1.Background = new SolidColorBrush(Color.FromArgb(255, 25, 31, 38));
            ExpanderControl1.BorderBrush = Brushes.Red;

            ExpanderControl1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ExpanderControl1.FontWeight = FontWeights.Medium;

            ExpanderControl1.IsExpanded = !ExpanderControl1.IsExpanded;






            PrevExpander = ExpanderControl1;
        }
        private Boolean IsCollapse = false;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (IsCollapse)
            {


                GridLengthAnimation gla = new GridLengthAnimation();
                gla.From = new GridLength(44);
                gla.To = new GridLength(230);
                gla.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                varun.BeginAnimation(ColumnDefinition.WidthProperty, gla);
                IsCollapse = false;
            }
            else
            {
                GridLengthAnimation gla = new GridLengthAnimation();
                gla.From = new GridLength(230);
                gla.To = new GridLength(44);
                gla.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                varun.BeginAnimation(ColumnDefinition.WidthProperty, gla);
                IsCollapse = true;
            }

        }
    }
}
