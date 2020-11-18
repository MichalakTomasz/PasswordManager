using PasswordManager.Context;
using PasswordManager.EntityModels;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Services
{
    public class DataService : IDataService
    {
        private readonly PasswordDbContext _context;
        private readonly ILogService _logService;
        private readonly IAppStateService _commonService;

        public DataService(PasswordDbContext passwordDbContext, ILogService logService, 
            IAppStateService commonService)
        {
            _context = passwordDbContext;
            _logService = logService;
            _commonService = commonService;
        }

        public bool AddPassword(PasswordSet passwordSet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passwordSet?.EncryptedPassword) || 
                    string.IsNullOrWhiteSpace(passwordSet?.Username) || 
                    string.IsNullOrWhiteSpace(passwordSet.Name))
                    throw new ArgumentNullException();

                using (var context = _context)
                {
                    context.Add(passwordSet);
                    context.SaveChanges();
                }

                return true;
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

        public IEnumerable<PasswordSet> GetPasswords()
        {
            using (var context = _context)
            {
                return context.Passwords.ToList();
            }
        }

        public bool CheckCredentials(Credentials credentials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(credentials?.Login) ||
                    string.IsNullOrWhiteSpace(credentials.Password))
                    throw new ArgumentNullException();

                using var context = _context;
                var exist = _context.Users.Any(u =>
                u.Username == credentials.Login.Trim().ToUpper() && 
                u.EncryptedPassword == credentials.Password);
                
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

        public bool ExistUser(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentNullException();

                using var context = _context;
                var exist = _context.Users.Any(u =>
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
