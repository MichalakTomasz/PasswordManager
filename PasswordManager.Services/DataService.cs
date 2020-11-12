using PasswordManager.Context;
using PasswordManager.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Services
{
    public class DataService : IDataService
    {
        private readonly PasswordDbContext _passwordDbContext;

        public DataService(PasswordDbContext passwordDbContext)
        {
            _passwordDbContext = passwordDbContext;
        }
        public IEnumerable<PasswordSet> GetPasswords()
        {
            using (var passwords = _passwordDbContext)
            {
                return _passwordDbContext.Passwords.ToList();
            }
        }
    }
}
