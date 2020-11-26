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
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private DelegateCommand _findUserCommand;
        public DelegateCommand FindUserCommand =>
            _findUserCommand ?? (_findUserCommand = 
            new DelegateCommand(ExecuteFindUserCommand, CanExecuteFindUserCommand));

        void ExecuteFindUserCommand()
        {

        }

        bool CanExecuteFindUserCommand()
        {
            return true;
        }
        private DelegateCommand _acceptCommand;
        public DelegateCommand AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = 
            new DelegateCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand));

        void ExecuteAcceptCommand()
        {
            RequestClose.Invoke(null);
        }

        bool CanExecuteAcceptCommand()
        {
            return true;
        }
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
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
