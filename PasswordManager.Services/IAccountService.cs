using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IAccountService
    {
        string LoggedUser { get; }
        bool IsLogged { get; }

        bool Login(Credentials credentials);
        void Logout();
        bool Register(Credentials credentials);
        bool RemindPassword(string username);
    }
}