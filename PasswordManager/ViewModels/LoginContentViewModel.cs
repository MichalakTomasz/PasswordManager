using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PasswordManager.ViewModels
{
    public class LoginContentViewModel : BindableBase, IDialogAware
    {
        public LoginContentViewModel(IExitService exitService)
        {
            _exitService = exitService;

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
            _loginCommand ?? (_loginCommand = 
            new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand)
            .ObservesProperty(() => Login)
            .ObservesProperty(() => Password));

        void ExecuteLoginCommand()
        {
            var param = new DialogParameters
            {
                {Literals.Login, Login },
                {Literals.Password, Password }
            };
            var dialogResult = new DialogResult(ButtonResult.OK, param);
            RequestClose.Invoke(dialogResult);
        }

        bool CanExecuteLoginCommand()
            => Login?.Length > 0 && Password?.Length > 0;

        private DelegateCommand _exitCommand;
        private readonly IExitService _exitService;

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

        }

        private DelegateCommand _remindPasswordCommand;
        public DelegateCommand RemindPasswordCommand =>
            _remindPasswordCommand ?? (_remindPasswordCommand =
            new DelegateCommand(ExecuteRemindPasswordCommand));

        void ExecuteRemindPasswordCommand()
        {

        }
    }
}
