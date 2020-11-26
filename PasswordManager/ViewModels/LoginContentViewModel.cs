using PasswordManager.LoginContent.Views;
using PasswordManager.Models;
using PasswordManager.Services;
using PasswordManager.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.ViewModels
{
    public class LoginContentViewModel : BindableBase, IDialogAware
    {
        public LoginContentViewModel(IExitService exitService, IAccountService accountService, 
            IDialogService dialogService)
        {
            _exitService = exitService;
            _accountService = accountService;
            _dialogService = dialogService;
            
            Title = Literals.Login;
        }
        public event Action<IDialogResult> RequestClose;

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _errorMessage;
        public string LoginErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public string Title { get; set; } 

        public bool CanCloseDialog()
            => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ??= 
            new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand)
            .ObservesProperty(() => Login)
            .ObservesProperty(() => Password);

        void ExecuteLoginCommand()
        {
            var credentials = new Credentials
            {
                Login = Login,
                Password = Password
            };
            var result = _accountService.Login(credentials);
            if (result)
                RequestClose.Invoke(null);
            else
                LoginErrorMessage = Literals.MessageLoginError;
        }

        bool CanExecuteLoginCommand()
            => Login?.Length > 0 && Password?.Length > 0;

        private DelegateCommand _exitCommand;
        private readonly IExitService _exitService;
        private readonly IAccountService _accountService;
        private readonly IDialogService _dialogService;

        public DelegateCommand ExitCommand =>
            _exitCommand ?? (_exitCommand = 
            new DelegateCommand(ExecuteExitCommand));

        void ExecuteExitCommand()
            => _exitService.AppExit();
        private DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand =
            new DelegateCommand(ExecuteRegisterCommand));

        void ExecuteRegisterCommand()
        {
            _dialogService.ShowDialog(nameof(RegisterContent));
        }

        private DelegateCommand _recoverPasswordCommand;
        public DelegateCommand RecoverPasswordCommand =>
            _recoverPasswordCommand ?? (_recoverPasswordCommand =
            new DelegateCommand(ExecuteRecoverPasswordCommand));

        void ExecuteRecoverPasswordCommand()
        {
            _dialogService.ShowDialog(nameof(RecoverPasswordContent));
        }

        private DelegateCommand<RoutedEventArgs> _passwordCommand;
        public DelegateCommand<RoutedEventArgs> PasswordCommand =>
            _passwordCommand ?? (_passwordCommand = new DelegateCommand<RoutedEventArgs>(ExecutePasswordCommnad));

        void ExecutePasswordCommnad(RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            Password = passwordBox.Password;
        }
    }
}
