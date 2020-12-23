using PasswordManager.MessageDialogContent.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PasswordManager.MessageDialogContent
{
    public class MessageDialogContentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageDialog>();
        }
    }
}