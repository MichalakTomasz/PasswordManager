using PasswordManager.EntityModels;
using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IAccountService
    {
        User LoggedUser { get; }
        bool IsLogged { get; }

        bool Login(Credentials credentials);
        void Logout();
        void Register(RegisterData registerData);
        bool RecoverPassword(string username);
    }
}