using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IAccountService
    {
        string LoggedUser { get; }
        bool IsLogged { get; }

        bool Login(Credentials credentials);
        void Logout();
        void Register(RegisterData registerData);
        bool RemindPassword(string username);
    }
}