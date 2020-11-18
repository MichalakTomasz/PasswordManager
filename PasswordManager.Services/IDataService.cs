using PasswordManager.EntityModels;
using PasswordManager.Models;
using System.Collections.Generic;

namespace PasswordManager.Services
{
    public interface IDataService
    {
        IEnumerable<PasswordSet> GetPasswords();
        bool AddPassword(PasswordSet passwordSet);
        bool CheckCredentials(Credentials credentials);
        bool ExistUser(string username);
    }
}