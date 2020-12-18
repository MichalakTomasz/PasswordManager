using PasswordManager.EntityModels;
using PasswordManager.Models;
using System.Collections.Generic;

namespace PasswordManager.Services
{
    public interface IDataService
    {
        IEnumerable<PasswordWrapper> GetPasswords();
        void AddPassword(PasswordWrapper passwordWrapper, User user);
        void DeletePassword(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        User GetUser(string username);
        bool UserExist(string username);
    }
}