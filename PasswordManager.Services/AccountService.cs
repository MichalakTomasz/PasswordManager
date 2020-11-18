using PasswordManager.Context;
using PasswordManager.Models;
using System;
using System.Linq;

namespace PasswordManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly PasswordDbContext _context;
        private readonly IAppStateService _appStateService;
        private readonly ILogService _logService;

        public string LoggedUser { get; set; }
        public bool IsLogged { get; set; }
        public AccountService(PasswordDbContext context, IAppStateService appStateService, ILogService logService)
        {
            _context = context;
            _appStateService = appStateService;
            _logService = logService;
            IsLogged = false;
        }
        public bool Login(Credentials credentials)
        {
            try
            {
                if (credentials == null ||
                string.IsNullOrWhiteSpace(credentials.Login) ||
                string.IsNullOrWhiteSpace(credentials.Password))
                    throw new ArgumentNullException();

                var login = credentials.Login.Trim().ToUpper();
                var password = credentials.Password.Trim();
                var result = _context.Users.Any(f =>
                f.Username == login && f.EncryptedPassword == password);

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
