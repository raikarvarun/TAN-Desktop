using System.Windows;
using System.Windows.Controls;

namespace TAN.Controls
{

    public class GrowthDirectionControl : Control
    {
        public string GrowthText
        {
            get { return (string)GetValue(GrowthTextProperty); }
            set { SetValue(GrowthTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GrowthText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GrowthTextProperty =
            DependencyProperty.Register("GrowthText", typeof(string), typeof(GrowthDirectionControl), new PropertyMetadata(string.Empty));




        public string ActualText
        {
            get { return (string)GetValue(ActualTextProperty); }
            set { SetValue(ActualTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActualText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualTextProperty =
            DependencyProperty.Register("ActualText", typeof(string), typeof(GrowthDirectionControl), new PropertyMetadata(string.Empty));


        static GrowthDirectionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GrowthDirectionControl), new FrameworkPropertyMetadata(typeof(GrowthDirectionControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }



    }
}
