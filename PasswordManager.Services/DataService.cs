﻿using Microsoft.EntityFrameworkCore;
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
        private readonly IAppStateService _appStateService;
        private readonly IAesCryptographicService _aesCryptographicService;
        private readonly IDataBinarySerializeService _dataBinarySerializeService;

        public DataService(DbContextService dbContextService, ILogService logService, 
            IAppStateService appStateService, IAesCryptographicService aesCryptographicService,
            IDataBinarySerializeService dataBinarySerializeService)
        {
            _dbContextService = dbContextService;
            _logService = logService;
            _appStateService = appStateService;
            _aesCryptographicService = aesCryptographicService;
            _dataBinarySerializeService = dataBinarySerializeService;

            CreateDatabaseIfNeeded();
        }

        private void CreateDatabaseIfNeeded()
        {
            using var context = _dbContextService.GetContext();
            context.Database.Migrate();
        }

        public void AddPassword(PasswordWrapper passwordWrapper, User user)
        {
            try
            {
                if (passwordWrapper == null || user == null)
                    throw new ArgumentNullException();

                var passwordBuffer = _dataBinarySerializeService.Serialize<string>(passwordWrapper.Password);
                var encryptedPassword = _aesCryptographicService.Encrypt(passwordBuffer);
                
                var password = new PasswordSet
                {
                    User = user,
                    Username = passwordWrapper.Username,
                    Name = passwordWrapper.Name,
                    Comment = passwordWrapper.Comment,
                    EncryptedPassword = encryptedPassword
                };
                using var context = _dbContextService.GetContext();
                context.Attach(password);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_appStateService.IsInDebugMode)
                    throw;
            }
        }
        public void UpdatePassword(PasswordWrapper editedPasswordWrapper, User user)
        {
            try
            {
                if (editedPasswordWrapper == null || user == null)
                    throw new ArgumentNullException();

                using var context = _dbContextService.GetContext();
                var toEdit = context.Passwords.FirstOrDefault(p => p.Id == editedPasswordWrapper.Id);
                if (toEdit != null)
                {
                    var passwordBuffer = _dataBinarySerializeService.Serialize<string>(editedPasswordWrapper.Password);
                    var encryptedPassword = _aesCryptographicService.Encrypt(passwordBuffer);
                    var passwordSet = new PasswordSet
                    {
                        Id = editedPasswordWrapper.Id,
                        Name = editedPasswordWrapper.Name,
                        Username = editedPasswordWrapper.Username,
                        EncryptedPassword = encryptedPassword,
                        Comment = editedPasswordWrapper.Comment,
                        User = user
                    };
                    context.Update(passwordSet);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_appStateService.IsInDebugMode)
                    throw;
            }
        }
        public void DeletePassword(int id)
        {
            try
            {
                using var context = _dbContextService.GetContext();
                var password = context.Passwords.FirstOrDefault(f => f.Id == id);
                if (password != null)
                {
                    context.Remove(password);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_appStateService.IsInDebugMode)
                    throw;
                throw;
            }
        }

        public void AddUser(User user)
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
                if (_appStateService.IsInDebugMode)
                    throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException();

                using var context = _dbContextService.GetContext();
                context.Update(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                _logService.LogError(e);
                if (_appStateService.IsInDebugMode)
                    return;
                else
                    throw;
            }
        }

        public IEnumerable<PasswordWrapper> GetPasswords()
        {
            using var context = _dbContextService.GetContext();
            var passwords = context.Passwords.ToList();
            var passwordModels = new List<PasswordWrapper>();
            passwords.ForEach(passwordSet =>
            {
                var encryptedPasswordBuffer = _aesCryptographicService.Decrypt(passwordSet.EncryptedPassword);
                var password = _dataBinarySerializeService.Deserialize<string>(encryptedPasswordBuffer);
                var passWordModel = new PasswordWrapper
                {
                    Id = passwordSet.Id,
                    Name = passwordSet.Name,
                    Username = passwordSet.Username,
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
                if (_appStateService.IsInDebugMode)
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
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }
    }
}
