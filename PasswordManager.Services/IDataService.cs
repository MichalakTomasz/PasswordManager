using PasswordManager.EntityModels;
using PasswordManager.Models;
using System.Collections.Generic;

namespace PasswordManager.Services
{
    public interface IDataService
    {
        IEnumerable<PasswordSet> GetPasswords();
        void SavePassword(PasswordSet passwordSet);
        void SaveUser(User user);
        User GetUser(string username);
        bool UserExist(string username);
    }
}