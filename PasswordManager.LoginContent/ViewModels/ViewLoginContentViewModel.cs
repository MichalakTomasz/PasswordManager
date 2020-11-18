using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PasswordManager.LoginContent.ViewModels
{
    public class ViewLoginContentViewModel : BindableBase, IDialogAware
    {
        private string _message;

        public event Action<IDialogResult> RequestClose;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public string Title { get; set; }

        public ViewLoginContentViewModel()
        {
            Title = "Test";
        }

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
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand));

        void ExecuteLoginCommand()
        {
            var param = new DialogParameters
            {
                {"Login", "Tomas" },
                {"Password", "1234" }
            };
            var dialogResult = new DialogResult(ButtonResult.OK, param);
            RequestClose.Invoke(dialogResult);
        }

        bool CanExecuteLoginCommand()
        {
            return true;
        }
    }
}
