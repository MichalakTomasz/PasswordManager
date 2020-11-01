using Prism.Ioc;
using PasswordManager.Views;
using System.Windows;
using PasswordManager.Services;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGeneratorService, GeneratorService>();
        }
    }
}
