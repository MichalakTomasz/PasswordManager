using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataGridPasswordColumn
{
    public class DataGridPasswordColumn : DataGridTextColumn
    {
        private object _content;
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            cell.PreviewTextInput += Cell_PreviewTextInput;

            return base.GenerateElement(cell, dataItem);
        }

        private void Cell_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Password = e.Text;
        }
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), 
                typeof(DataGridPasswordColumn), new PropertyMetadata(null));

        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), 
                typeof(DataGridPasswordColumn), new PropertyMetadata((char)248, OnPasswordCharChanged));

        private static void OnPasswordCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGridPasswordColumn = d as DataGridPasswordColumn;
            if (!dataGridPasswordColumn.IsPasswordVisible)
            {
                var passwordLength = dataGridPasswordColumn.Password.Length;
                ReloadPasswordMask(dataGridPasswordColumn, passwordLength);
            }
        }

        public bool IsPasswordVisible
        {
            get { return (bool)GetValue(IsPasswordVisibleProperty); }
            set { SetValue(IsPasswordVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsPasswordVisibleProperty =
            DependencyProperty.Register("IsPasswordVisible", typeof(bool), 
                typeof(DataGridPasswordColumn), new PropertyMetadata(false, OnIsPasswordVisibleChanged));
        
        private static void OnIsPasswordVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGridPasswordColumn = d as DataGridPasswordColumn;
            var length = dataGridPasswordColumn.Password.Length;
            if ((bool)e.NewValue)
                dataGridPasswordColumn._content = dataGridPasswordColumn.Password;
            else
                ReloadPasswordMask(dataGridPasswordColumn, length);
        }

        private static void ReloadPasswordMask(DataGridPasswordColumn dataGridPasswordColumn, int length)
        {
            var password = new StringBuilder();
            Enumerable.Range(0, length).ToList().ForEach(a =>
                password.Append(dataGridPasswordColumn.PasswordChar.ToString()));
            dataGridPasswordColumn._content = password.ToString();
        }
    }
}
