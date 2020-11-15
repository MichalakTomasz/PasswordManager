using Prism.Ioc;
using PasswordManager.Views;
using System.Windows;
using PasswordManager.Services;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Context;

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
            containerRegistry.Register<IDataService, DataService>();
            containerRegistry.Register<ILogService, LogService>();
            containerRegistry.Register<IAppStateService, AppStateService>();
            containerRegistry.Register<IDataBinaryConverterService, DataBinaryConverterService>();
            containerRegistry.Register<IAesCryptographicService, AesCryptographicService>();

            var dbContextOptions = GetDbcontextOptions();
            containerRegistry.RegisterInstance(new PasswordDbContext(dbContextOptions));
        }

        private DbContextOptions<PasswordDbContext> GetDbcontextOptions()
        {
            var connectionString = (@"Data Source=..\PasswordDbSqlite.db");
            var dbContextBuilder = new DbContextOptionsBuilder<PasswordDbContext>();
            dbContextBuilder.UseSqlite(connectionString);
            dbContextBuilder.EnableSensitiveDataLogging(true);
            return dbContextBuilder.Options;
        }

        //private string InjectPathToConnectionString(string path)
        //    => ConfigurationManager.ConnectionStrings["DbFakturaSqlite"]
        //    .ConnectionString.Replace("%path%", path);
    }
}
