using LoginPopupContent.ViewModels;
using LoginPopupContent.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LoginPopupContent
{
    public class LoginPopupContentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ViewLoginContent, ViewLoginContentViewModel>("loginContent");
        }
    }
}