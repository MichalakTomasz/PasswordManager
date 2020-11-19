namespace PasswordManager.Services
{
    public interface IGenericCryptographicService
    {
        byte[] Decrypt(byte[] buffer);
        byte[] Encrypt(byte[] buffer);
    }
}