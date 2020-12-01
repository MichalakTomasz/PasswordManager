using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.ViewModels
{
    public class ChangePasswordContentViewModel : BindableBase, IDialogAware
    {
        public ChangePasswordContentViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set { SetProperty(ref _oldPassword, value); }
        }
        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { SetProperty(ref _newPassword, value); }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private DelegateCommand<RoutedEventArgs> _oldPasswordCommand;
        public DelegateCommand<RoutedEventArgs> OldPasswordCommand =>
            _oldPasswordCommand ?? (_oldPasswordCommand = 
            new DelegateCommand<RoutedEventArgs>(ExecuteOldPasswordCommand));

        void ExecuteOldPasswordCommand(RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            OldPassword = passwordBox.Password;
        }
        private DelegateCommand<RoutedEventArgs> _newPasswordCommand;
        public DelegateCommand<RoutedEventArgs> NewPasswordCommand =>
            _newPasswordCommand ?? (_newPasswordCommand = 
            new DelegateCommand<RoutedEventArgs>(ExecuteNewPasswordCommand));

        void ExecuteNewPasswordCommand(RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            NewPassword = passwordBox.Password;
        }
        private DelegateCommand<RoutedEventArgs> _confirmNewPasswordCommand;
        public DelegateCommand<RoutedEventArgs> ConfirmNewPasswordCommand =>
            _confirmNewPasswordCommand ?? (_confirmNewPasswordCommand = 
            new DelegateCommand<RoutedEventArgs>(ExecuteConfirmNewPasswordCommand));

        void ExecuteConfirmNewPasswordCommand(RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            ConfirmPassword = passwordBox.Password;
        }

        private DelegateCommand _backCommand;
        public DelegateCommand BackCommand =>
            _backCommand ?? (_backCommand = 
            new DelegateCommand(ExecuteBackCommand));
        void ExecuteBackCommand()
            => RequestClose.Invoke(null);

        private DelegateCommand _changePasswordCommand;
        private readonly IAccountService _accountService;

        public DelegateCommand ChangePasswordCommand =>
            _changePasswordCommand ?? (_changePasswordCommand = 
            new DelegateCommand(ExecuteChangePasswordCommand, CanExecuteChangePasswordCommand)
            .ObservesProperty(() => Username)
            .ObservesProperty(() => OldPassword)
            .ObservesProperty(() => NewPassword)
            .ObservesProperty(() => ConfirmPassword));

        void ExecuteChangePasswordCommand()
        {
            var changePasswordModel = new ChangePasswordModel
            {
                Username = Username,
                OldPassword = OldPassword,
                NewPassword = NewPassword
            };
            var result = _accountService.ChangePassword(changePasswordModel);
            if (result.ResultType == ResultType.Success)
            {
                RequestClose.Invoke(null);
            }
            else
                ErrorMessage = result.ErrorMessage;
        }

        bool CanExecuteChangePasswordCommand()
            => !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(OldPassword) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            !string.IsNullOrWhiteSpace(ConfirmPassword) &&
            NewPassword == ConfirmPassword;
        

        public string Title => "Change password";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
            => true;

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
