using PasswordManager.BaseClasses;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class PasswordWrapper : WrapperBase
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
        public bool IsChangedName => GetIsChanged(nameof(Name));
        public string OriginalValueName => GetOriginalValue<string>(nameof(Name));

        private string _username;
        [RegularExpression(Literals.RegexLettersDigits)]
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
        public bool IsChangedPassword => GetIsChanged(nameof(Password));
        public string OriginalValuePassword => GetOriginalValue<string>(nameof(Password));
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        public bool IsChangedComment => GetIsChanged(nameof(Comment));
        public string OriginalValueComment => GetOriginalValue<string>(nameof(Comment));

        private bool _isVisiblePassword;
        public bool IsVisiblePassword
        {
            get { return _isVisiblePassword; }
            set { SetProperty(ref _isVisiblePassword, value); }
        }

        private DelegateCommand _passwordVisibilityChangingCommand;
        public DelegateCommand PasswordVisibilityChangingCommand =>
            _passwordVisibilityChangingCommand ?? (_passwordVisibilityChangingCommand =
            new DelegateCommand(ExecutePasswordVisibilityChangingCommand));

        void ExecutePasswordVisibilityChangingCommand()
            => IsVisiblePassword = !IsVisiblePassword;
    }
}
