using PasswordManager.EntityModels;
using System.Collections.Generic;

namespace PasswordManager.Services
{
    public interface IDataService
    {
        IEnumerable<PasswordSet> GetPasswords();
    }
}