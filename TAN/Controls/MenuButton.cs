






//Not Working













using System.Windows;
using System.Windows.Controls;
using TAN.EventModels;

namespace TAN.Controls
{
    [TemplatePart(Name = "ButtonMenu", Type = typeof(Button))]
    [TemplatePart(Name = "change1", Type = typeof(StackPanel))]

    public class MenuButton : Control
    {

        public string PathData1
        {
            get { return (string)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PathData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathDataProperty =
            DependencyProperty.Register("PathData1", typeof(string), typeof(MenuButton), new PropertyMetadata(default));

        public delegate void MenuButtonClickedEventHandler(object sender, MenuButtonClickEventArgs args);
        static MenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuButton), new FrameworkPropertyMetadata(typeof(MenuButton)));

        }
        private Button _buttonMenu;
        private StackPanel _path;
        public override void OnApplyTemplate()
        {
            _buttonMenu = this.Template.FindName("ButtonMenu", this) as Button;
            _path = this.Template.FindName("change1", this) as StackPanel;

            _buttonMenu.Click += (s, e) => { OnMenuButtonClicked(); };


        }


        public Point PathData2
        {
            get { return (Point)GetValue(PathDataProperty2); }
            set { SetValue(PathDataProperty2, value); }
        }

        // Using a DependencyProperty as the backing store for PathData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathDataProperty2 =
            DependencyProperty.Register("PathData2", typeof(Point), typeof(MenuButton), new PropertyMetadata(default));


        public static RoutedEvent MenuButtonClikcedEvent = EventManager.RegisterRoutedEvent("MenuButtonClikced",
             RoutingStrategy.Bubble, typeof(MenuButtonClickedEventHandler), typeof(MenuButton));

        public event MenuButtonClickedEventHandler MenuButtonClikced
        {
            add
            {
                AddHandler(MenuButtonClikcedEvent, value);
            }
            remove
            {
                RemoveHandler(MenuButtonClikcedEvent, value);
            }
        }


        protected virtual void OnMenuButtonClicked()
        {


            RaiseEvent(new MenuButtonClickEventArgs(MenuButtonClikcedEvent, this) { CurrentButton = _buttonMenu });
        }









    }
}
