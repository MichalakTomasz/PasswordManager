using PasswordManager.BaseClasses;
using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;

namespace PasswordManager.EditPasswordContent.ViewModels
{
    public class EditPasswordViewModel : ValidatableBase, IDialogAware
    {
        public EditPasswordViewModel(IDataService dataService, IAccountService accountService)
        {
            _dataService = dataService;
            _accountService = accountService;
        }
        private string _name;
        private PasswordWrapper _passwordWrapper;

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
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand));

        void ExecuteSaveCommand()
        {
            _passwordWrapper.Name = Name;
            _passwordWrapper.Username = Username;
            _passwordWrapper.Password = Password;
            _passwordWrapper.Comment = Comment;
            _dataService.UpdatePassword(_passwordWrapper, _accountService.LoggedUser);
            var dialogResult = new DialogResult(ButtonResult.OK);
            RequestClose.Invoke(dialogResult);
        }

        bool CanExecuteSaveCommand()
        {
            return true;
        }
        private DelegateCommand _cancelCommand;
        private readonly IDataService _dataService;
        private readonly IAccountService _accountService;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        public string Title => "Edit password data";

        void ExecuteCancelCommand()
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
            var passwordWrapper = parameters.GetValue<PasswordWrapper>(nameof(PasswordWrapper));
            if (passwordWrapper != null)
            {
                _passwordWrapper = passwordWrapper;
                Name = passwordWrapper.Name;
                Username = passwordWrapper.Name;
                Password = passwordWrapper.Password;
                Comment = passwordWrapper.Comment;
            }
        }
    }
}
