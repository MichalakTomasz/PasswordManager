using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace LoginPopupContent.ViewModels
{
    public class ViewLoginContentViewModel : BindableBase, IDialogAware
    {
        public ViewLoginContentViewModel()
        {
            Title = "Test";
        }
        public string Title { get; set; }

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
    }
}
