using PasswordManager.EditPasswordContent.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PasswordManager.EditPasswordContent
{
    public class EditPasswordContentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<EditPassword>();
        }
    }
}