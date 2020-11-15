using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IAesCryptographicService
    {
        AesKey Key { get; set; }

        byte[] Decrypt(byte[] encryptedBuffer);
        byte[] Encrypt(byte[] buffer);
        AesKey GenerateKey();
    }
}