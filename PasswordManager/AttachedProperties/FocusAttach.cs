using System.Windows;

namespace PasswordManager.AttachedProperties
{
    public class FocusAttach
    {


        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(FocusAttach), new PropertyMetadata(false, OnIsFocusedChanged));

        private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = d as UIElement;
            if (uiElement != null)
                if ((bool)e.NewValue)  uiElement.Focus();
        }
    }
}
