using Prism.Mvvm;

namespace PasswordManager.Models
{
    public class PasswordModel : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        private bool _isVisiblePassword;
        public bool IsVisiblePassword
        {
            get { return _isVisiblePassword; }
            set { SetProperty(ref _isVisiblePassword, value); }
        }
    }
}
