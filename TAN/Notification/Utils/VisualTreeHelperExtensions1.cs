using System.Windows;
using System.Windows.Media;

namespace TAN.Notification.Utils
{
    internal class VisualTreeHelperExtensions1
    {
        public static T GetParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            if (parent == null) return null;

            var tParent = parent as T;
            if (tParent != null)
            {
                return tParent;
            }

            return GetParent<T>(parent);
        }
    }
}
