using Prism.Ioc;
using PasswordManager.Views;
using System.Windows;
using PasswordManager.Services;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Context;
using System.Configuration;
using Prism.Modularity;
using PasswordManager.LoginContent;
using Prism.Services.Dialogs;
using PasswordManager.ViewModels;
using PasswordManager.Models;

namespace PasswordManager
{
    public partial class App
    {
        private IAccountService _loginServie;

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGeneratorService, GeneratorService>();
            containerRegistry.Register<IDataService, DataService>();
            containerRegistry.Register<ILogService, LogService>();
            containerRegistry.Register<IAppStateService, AppStateService>();
            containerRegistry.Register<IDataBinarySerializeService, DataBinarySerializeService>();
            containerRegistry.Register<IAesCryptographicService, AesCryptographicService>();
            containerRegistry.Register<IExitService, ExitService>();
            containerRegistry.Register<ICopyService, CopyService>();
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();
            var dbContextOptions = GetDbcontextOptions();
            containerRegistry.RegisterInstance<DbContextService>(new DbContextService(dbContextOptions));
            containerRegistry.Register<IGenericCryptographicService, GenericCryptographicService>();

            
            containerRegistry.RegisterDialog<Views.LoginContent, LoginContentViewModel>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<LoginContentModule>();
        }

        private DbContextOptions<PasswordDbContext> GetDbcontextOptions()
        {
            var dbPath = @"..\PasswordDbSqlite.db";
            var connectionString = InjectPathToConnectionString(dbPath);
            var dbContextBuilder = new DbContextOptionsBuilder<PasswordDbContext>();
            dbContextBuilder.UseSqlite(connectionString);
            dbContextBuilder.EnableSensitiveDataLogging(true);
            return dbContextBuilder.Options;
        }

        private string InjectPathToConnectionString(string path)
            => ConfigurationManager.ConnectionStrings["sqlite"]
            .ConnectionString.Replace("%path%", path);

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
            var dialogService = Container.Resolve<IDialogService>();
            _loginServie = Container.Resolve<IAccountService>();
            dialogService.ShowDialog(nameof(LoginContent), LoginDialogCallBack);
        }

        private void LoginDialogCallBack(IDialogResult dialogResult)
        {
            var Login = dialogResult.Parameters.GetValue<string>(Literals.Login);
            var password = dialogResult.Parameters.GetValue<string>(Literals.Password);
            var credentials = new Credentials
            {
                Login = Login,
                Password = password
            };
            _loginServie.Login(credentials);
        }
    }
}
