using PasswordManager.LoginContent.ViewModels;
using PasswordManager.LoginContent.Views;
using PasswordManager.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PasswordManager.LoginContent
{
    public class LoginContentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ViewLoginContent, ViewLoginContentViewModel>();
        }
    }
}