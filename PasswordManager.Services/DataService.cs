using PasswordManager.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Services
{
    public class DataService : IDataService
    {
        private readonly DbContextService _dbContextService;
        private readonly ILogService _logService;
        private readonly IAppStateService _commonService;

        public DataService(DbContextService dbContextService, ILogService logService, 
            IAppStateService commonService)
        {
            _dbContextService = dbContextService;
            _logService = logService;
            _commonService = commonService;
        }

        public void SavePassword(PasswordSet passwordSet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passwordSet?.EncryptedPassword) ||
                    string.IsNullOrWhiteSpace(passwordSet?.Username) ||
                    string.IsNullOrWhiteSpace(passwordSet.Name) ||
                    passwordSet.User == null)
                    throw new ArgumentNullException();

                using var context = _dbContextService.GetContext();
                context.Add(passwordSet);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_commonService.IsInDebugMode)
                    throw;
            }
        }

        public void SaveUser(User user)
        {
            try
            {
                if (user == null ||
                    string.IsNullOrWhiteSpace(user.Username) ||
                    user.EncryptedPassword.Length == 0)
                    throw new ArgumentException();

                using var context = _dbContextService.GetContext();
                context.Add(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_commonService.IsInDebugMode)
                    throw;
            }
        }

        public IEnumerable<PasswordSet> GetPasswords()
        {
            using (var context = _dbContextService.GetContext())
            {
                return context.Passwords.ToList();
            }
        }

        public User GetUser(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentNullException();

                using var context = _dbContextService.GetContext();
                var user = context.Users.FirstOrDefault(u => 
                u.Username == username.Trim().ToUpper());
                
                return user;
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_commonService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }

        public bool UserExist(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentNullException();

                using var context = _dbContextService.GetContext();
                var exist = context.Users.Any(u =>
                u.Username == username.Trim().ToUpper());
                return exist;
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_commonService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }
    }
}
