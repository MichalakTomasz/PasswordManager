using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IGeneratorService
    {
        string GenerateKey(int length, KeyTypes keyType);
    }
}