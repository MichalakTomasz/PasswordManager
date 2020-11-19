using PasswordManager.Context;

namespace PasswordManager.Services
{
    public interface IDbContextService
    {
        PasswordDbContext GetContext();
    }
}