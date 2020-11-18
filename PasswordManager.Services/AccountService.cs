using PasswordManager.Context;
using PasswordManager.Models;
using System;
using System.Linq;

namespace PasswordManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataService _dataService;
        private readonly IAppStateService _appStateService;
        private readonly ILogService _logService;

        public string LoggedUser { get; private set; }
        public bool IsLogged { get; private set; }
        public AccountService(IDataService dataService, IAppStateService appStateService, ILogService logService)
        {
            _dataService = dataService;
            _appStateService = appStateService;
            _logService = logService;
            IsLogged = false;
        }
        public bool Login(Credentials credentials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(credentials?.Login) ||
                    string.IsNullOrWhiteSpace(credentials.Password))
                    throw new ArgumentNullException();

                var login = credentials.Login.Trim().ToUpper();
                var password = credentials.Password.Trim();
                var result = _dataService.CheckCredentials(credentials);

                if (result)
                {
                    LoggedUser = login;
                    IsLogged = true;
                    return true;
                }
                else
                {
                    LoggedUser = null;
                    IsLogged = false;
                    return false;
                }
            }
            catch (Exception e)
            {
                _logService.LogError($"Account service Login error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return false;
            }
        }

        public void Logout()
        {
            LoggedUser = null;
            IsLogged = false;
        }

        public bool Register(Credentials credentials)
        {
            throw new System.NotImplementedException();
        }

        public bool RemindPassword(string username)
        {
            throw new NotImplementedException();
        }
    }
}
