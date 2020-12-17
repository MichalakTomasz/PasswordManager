using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomPasswordBox
{
    /// <summary>
    /// Interaction logic for CustomPasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {
        public CustomPasswordBox()
        {
            InitializeComponent();
        }



        public bool IsVisiblePassword
        {
            get { return (bool)GetValue(IsVisiblePasswordProperty); }
            set { SetValue(IsVisiblePasswordProperty, value); }
        }

        public static readonly DependencyProperty IsVisiblePasswordProperty =
            DependencyProperty.Register("IsVisiblePassword", typeof(bool), typeof(CustomPasswordBox), new PropertyMetadata(false, OnIsVisiblePasswordChanged));

        private static void OnIsVisiblePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var customPasswordBox = d as CustomPasswordBox;
            if ((bool)e.NewValue)
                customPasswordBox.passwordBox.Visibility = Visibility.Hidden;
            else
                customPasswordBox.passwordBox.Visibility = Visibility.Visible;
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox), 
                new PropertyMetadata(null, OnPasswordChanged));

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var customPasswordBox = d as CustomPasswordBox;
            var value = e.NewValue.ToString();
            customPasswordBox.passwordBox.Password = value;
            customPasswordBox.textBox.Text = value;
        }
    }
}
