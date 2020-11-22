using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PasswordManager.ViewModels
{
    class RegisterContentViewModel : BindableBase, IDialogAware
    {
        public RegisterContentViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public string Title => "Rejestruj";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

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
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }
        private string _secondPassword;
        public string SecondPassword
        {
            get { return _secondPassword; }
            set { SetProperty(ref _secondPassword, value); }
        }
        private string _secondPasswordQuestion;
        public string SecondPasswordQuestion
        {
            get { return _secondPasswordQuestion; }
            set { SetProperty(ref _secondPasswordQuestion, value); }
        }

        private DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = 
            new DelegateCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand)
            .ObservesProperty(() => Login)
            .ObservesProperty(() => Password)
            .ObservesProperty(() => SecondPassword)
            .ObservesProperty(() => ConfirmPassword)
            .ObservesProperty(() => SecondPasswordQuestion));

        void ExecuteRegisterCommand()
        {
            var registerData = new RegisterData
            {
                Login = Login,
                Password = Password,
                SecondPassword = SecondPassword,
                SecondPasswordQuestion = SecondPasswordQuestion
            };
            _accountService.Register(registerData);
            RequestClose.Invoke(null);
        }

        bool CanExecuteRegisterCommand()
            => !string.IsNullOrWhiteSpace(Login) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(ConfirmPassword) &&
            !string.IsNullOrWhiteSpace(SecondPassword) &&
            !string.IsNullOrWhiteSpace(SecondPasswordQuestion) &&
            Password != null && Password.Equals(ConfirmPassword);

        private DelegateCommand _backCommand;
        private readonly IAccountService _accountService;

        public DelegateCommand BackCommand =>
            _backCommand ?? (_backCommand = new DelegateCommand(ExecuteBackCommand));

        void ExecuteBackCommand()
        {
            RequestClose.Invoke(null);
        }
    }
}
