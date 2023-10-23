using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TAN.Helpers
{
    public class Attacher : DependencyObject
    {
        public static readonly DependencyProperty
                   GeometryProperty = DependencyProperty.RegisterAttached(
                  "Geometry", typeof(string), typeof(Attacher), new PropertyMetadata("", GeometryChanged));

        private static void GeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var path = d as Path;
            if (path == null)
            {
                return;
            }

            path.Data = Geometry.Parse(e.NewValue.ToString());

        }
        public static string GetGeometry(DependencyObject d)
        {
            return (string)d.GetValue(GeometryProperty);
        }
        public static void SetGeometry(DependencyObject d, string value)
        {
            d.SetValue(GeometryProperty, value);
        }
    }
}
