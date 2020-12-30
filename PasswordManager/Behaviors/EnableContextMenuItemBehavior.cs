using Microsoft.Xaml.Behaviors;
using PasswordManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.Behaviors
{
    public class EnableContextMenuItemBehavior : Behavior<MenuItem>
    {
        public PasswordWrapper SelectedItem
        {
            get { return (PasswordWrapper)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(PasswordWrapper), 
                typeof(EnableContextMenuItemBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            var contextMenu = AssociatedObject.Parent as ContextMenu;
            if (contextMenu != null)
            contextMenu.Opened += ContextMenuOpened;
        }

        private void ContextMenuOpened(object sender, RoutedEventArgs e)
            => AssociatedObject.IsEnabled = SelectedItem != null;
    }
}
