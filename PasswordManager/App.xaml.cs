﻿using Microsoft.EntityFrameworkCore;
using PasswordManager.Context;
using PasswordManager.Services;
using PasswordManager.ViewModels;
using PasswordManager.Views;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Configuration;
using System.Windows;

namespace PasswordManager
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAesCryptographicService, AesCryptographicService>();
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();

            containerRegistry.Register<IGeneratorService, GeneratorService>();
            containerRegistry.Register<IDataService, DataService>();
            containerRegistry.Register<ILogService, LogService>();
            containerRegistry.Register<IAppStateService, AppStateService>();
            containerRegistry.Register<IDataBinarySerializeService, DataBinarySerializeService>();
            containerRegistry.Register<IExitService, ExitService>();
            containerRegistry.Register<ICopyService, CopyService>();
            
            var dbContextOptions = GetDbcontextOptions();
            containerRegistry.RegisterInstance<DbContextService>(new DbContextService(dbContextOptions));
            containerRegistry.Register<IGenericCryptographicService, GenericCryptographicService>();

            containerRegistry.RegisterDialog<Views.LoginContent, LoginContentViewModel>();
            containerRegistry.RegisterDialog<Views.RegisterContent, RegisterContentViewModel>();
            containerRegistry.RegisterDialog<Views.RecoverPasswordContent>();
            containerRegistry.RegisterDialog<Views.ChangePasswordContent>();
        }

        private DbContextOptions<PasswordDbContext> GetDbcontextOptions()
        {
            string dbPath = GetDatabasePath();
            var connectionString = InjectPathToConnectionString(dbPath);
            var dbContextBuilder = new DbContextOptionsBuilder<PasswordDbContext>();
            dbContextBuilder.UseSqlite(connectionString);
            dbContextBuilder.EnableSensitiveDataLogging(true);
            return dbContextBuilder.Options;
        }

        private string GetDatabasePath()
        {
            var dbPath = @"D:\Repos\PasswordManager\PasswordDbSqlite.db"; ;
            var appState = Container.Resolve<IAppStateService>();
            if (appState.IsInProductionMode)
                dbPath = "PasswordDb.db";
            return dbPath;
        }

        private string InjectPathToConnectionString(string path)
            => ConfigurationManager.ConnectionStrings["sqlite"]
            .ConnectionString.Replace("%path%", path);

        protected override void InitializeShell(Window shell)
        {
            var dialogService = Container.Resolve<IDialogService>();
            dialogService.ShowDialog(nameof(LoginContent));
        }
    }
}
