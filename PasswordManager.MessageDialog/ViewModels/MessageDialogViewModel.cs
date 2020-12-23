using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PasswordManager.MessageDialogContent.ViewModels
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public string Title { get; private set; }

        public bool CanCloseDialog()
            => true;

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var message = parameters.GetValue<string>("Message");
            if (!string.IsNullOrEmpty(message))
                Message = message;

            var title = parameters.GetValue<string>("Title");
            if (!string.IsNullOrEmpty(title))
                Title = title;
        }

        private DelegateCommand _yesCommand;
        public DelegateCommand YesCommand =>
            _yesCommand ?? (_yesCommand = new DelegateCommand(ExecuteYesCommand));

        void ExecuteYesCommand()
        {
            var dialogResult = new DialogResult(ButtonResult.Yes);
            RequestClose.Invoke(dialogResult);
        }
        private DelegateCommand _noCommand;
        public DelegateCommand NoCommand =>
            _noCommand ?? (_noCommand = new DelegateCommand(ExecuteNoCommand));

        void ExecuteNoCommand()
        {
            var dialogResult = new DialogResult(ButtonResult.No);
            RequestClose.Invoke(dialogResult);
        }
    }
}
