using PasswordManager.Context;
using PasswordManager.EntityModels;
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

        public DataService(PasswordDbContext passwordDbContext, ILogService logService, IAppStateService commonService)
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
                    return false;
            }
        }

        public IEnumerable<PasswordSet> GetPasswords()
        {
            using (var context = _context)
            {
                return context.Passwords.ToList();
            }
        }
    }
}
