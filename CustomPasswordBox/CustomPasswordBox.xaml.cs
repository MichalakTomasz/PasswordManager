using System.Windows;
using System.Windows.Controls;

namespace CustomPasswordBox
{
    public partial class CustomPasswordBox : UserControl
    {
        public CustomPasswordBox()
        {
            InitializeComponent();
            textBox.TextChanged += TextBox_TextChanged;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            if (passwordBox != null)
                Password = passwordBox.Password;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                Password = textBox.Text;
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
