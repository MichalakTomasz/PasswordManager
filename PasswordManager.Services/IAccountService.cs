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
        bool Register(RegisterModel registerData);
        PasswordData RecoverPassword(string username);
        IOperationResult ChangePassword(ChangePasswordModel changePasswordModel);
    }
}