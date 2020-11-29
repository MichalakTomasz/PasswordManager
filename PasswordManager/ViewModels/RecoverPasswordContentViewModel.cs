using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PasswordManager.ViewModels
{
    public class RecoverPasswordContentViewModel : BindableBase, IDialogAware
    {
        public RecoverPasswordContentViewModel(IAccountService accountService)
        {
            _accountService = accountService;

            IsVisibleUsernameArea = true;
            IsVisibleSecondPasswordArea = false;
            IsVisiblePasswordArea = false;
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _secondPassword;
        public string SecondPassword
        {
            get { return _secondPassword; }
            set { SetProperty(ref _secondPassword, value); }
        }
        private string _secondPasswordToCompare;
        public string SecondPasswordToCompare
        {
            get { return _secondPasswordToCompare; }
            set { SetProperty(ref _secondPasswordToCompare, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private string _secondPasswordQuestion;
        public string SecondPasswordQuestion
        {
            get { return _secondPasswordQuestion; }
            set { SetProperty(ref _secondPasswordQuestion, value); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }
        private bool _isVisibleSecondPasswordArea;
        public bool IsVisibleSecondPasswordArea
        {
            get { return _isVisibleSecondPasswordArea; }
            set { SetProperty(ref _isVisibleSecondPasswordArea, value); }
        }

        private bool _isVisiblePasswordArea;
        public bool IsVisiblePasswordArea
        {
            get { return _isVisiblePasswordArea; }
            set { SetProperty(ref _isVisiblePasswordArea, value); }
        }
        private bool _isVisibleUernameArew;
        public bool IsVisibleUsernameArea
        {
            get { return _isVisibleUernameArew; }
            set { SetProperty(ref _isVisibleUernameArew, value); }
        }

        private DelegateCommand _findUserCommand;
        public DelegateCommand FindUserCommand =>
            _findUserCommand ?? (_findUserCommand = 
            new DelegateCommand(ExecuteFindUserCommand, CanExecuteFindUserCommand)
            .ObservesProperty(() => Username));

        void ExecuteFindUserCommand()
        {
            var passwordData = _accountService.RecoverPassword(Username);
            if (passwordData != null)
            {
                Password = passwordData.Password;
                SecondPassword = passwordData.SecondPassword;
                SecondPasswordQuestion = passwordData.SecondPasswordQuestion;
                IsVisibleSecondPasswordArea = true;
                IsVisibleUsernameArea = false;
            }
            else
            {
                ErrorMessage = Literals.AccountNotExist;
            }
        }

        bool CanExecuteFindUserCommand()
            => !string.IsNullOrEmpty(Username);

        private DelegateCommand _acceptCommand;
        public DelegateCommand AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = 
            new DelegateCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand)
            .ObservesProperty(() => SecondPasswordToCompare));

        void ExecuteAcceptCommand()
        {
            if (SecondPasswordToCompare == SecondPassword)
            {
                IsVisiblePasswordArea = true;
                IsVisibleSecondPasswordArea = false;
                IsVisibleUsernameArea = false;
            }
            else
            {
                ErrorMessage = Literals.WrongPassword;
            }
        }

        bool CanExecuteAcceptCommand()
            => !string.IsNullOrEmpty(SecondPasswordToCompare);

        private DelegateCommand _backCommand;
        private readonly IAccountService _accountService;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand BackCommand =>
            _backCommand ?? (_backCommand = 
            new DelegateCommand(ExecuteBackCommand));

        public string Title => "Przywróć hasło";

        void ExecuteBackCommand()
        {
            RequestClose.Invoke(null);
        }

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
