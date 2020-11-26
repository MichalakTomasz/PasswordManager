using PasswordManager.EntityModels;
using PasswordManager.Models;
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
        private readonly IAesCryptographicService _aesCryptographicService;
        private readonly IDataBinarySerializeService _dataBinarySerializeService;

        public DataService(DbContextService dbContextService, ILogService logService, 
            IAppStateService commonService, IAesCryptographicService aesCryptographicService,
            IDataBinarySerializeService dataBinarySerializeService)
        {
            _dbContextService = dbContextService;
            _logService = logService;
            _commonService = commonService;
            _aesCryptographicService = aesCryptographicService;
            _dataBinarySerializeService = dataBinarySerializeService;
        }

        public void SavePassword(PasswordSet passwordSet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passwordSet?.Username) ||
                    string.IsNullOrWhiteSpace(passwordSet.Name) ||
                    passwordSet.User == null ||
                    passwordSet.EncryptedPassword?.Count() == 0)
                    throw new ArgumentNullException();

                using var context = _dbContextService.GetContext();
                context.Attach(passwordSet);
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

        public IEnumerable<PasswordModel> GetPasswords()
        {
            using var context = _dbContextService.GetContext();
            var passwords = context.Passwords.ToList();
            var passwordModels = new List<PasswordModel>();
            passwords.ForEach(passwordSet =>
            {
                var encryptedPasswordBuffer = _aesCryptographicService.Decrypt(passwordSet.EncryptedPassword);
                var password = _dataBinarySerializeService.Deserialize<string>(encryptedPasswordBuffer);
                var passWordModel = new PasswordModel
                {
                    Id = passwordSet.Id,
                    Name = passwordSet.Name,
                    Usermane = passwordSet.Username,
                    Password = password,
                    Comment = passwordSet.Comment
                };
                passwordModels.Add(passWordModel);
            });

            return passwordModels;
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
