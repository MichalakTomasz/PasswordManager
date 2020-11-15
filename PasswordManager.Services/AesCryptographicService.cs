using PasswordManager.Models;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager.Services
{
    public class AesCryptographicService : IAesCryptographicService
    {
        private readonly ILogService _logService;
        private readonly IAppStateService _appStateService;

        public AesKey Key { get; set; }
        public AesCryptographicService(ILogService logService, IAppStateService appStateService)
        {
            _logService = logService;
            _appStateService = appStateService;
        }

        public byte[] Encrypt(byte[] buffer)
        {
            try
            {
                using (var aesAlgoritm = new AesCryptoServiceProvider())
                {
                    aesAlgoritm.Key = Key.Key;
                    aesAlgoritm.IV = Key.IV;
                    ICryptoTransform encryptor =
                        aesAlgoritm.CreateEncryptor(aesAlgoritm.Key, aesAlgoritm.IV);
                    using (var encryptedMemoryStream = new MemoryStream())
                    using (var sourceStream = new MemoryStream(buffer))
                    {
                        using (var cryptoStream = new CryptoStream(
                            encryptedMemoryStream, encryptor, CryptoStreamMode.Write))
                            sourceStream.CopyTo(cryptoStream);
                        return encryptedMemoryStream.ToArray();
                    }
                }
            }
            catch (System.Exception e)
            {
                _logService.LogError($"{nameof(AesCryptographicService)} encrypt error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }

        public byte[] Decrypt(byte[] encryptedBuffer)
        {
            try
            {
                using (var decryptedStream = new MemoryStream())
                {
                    using (var aesAlg = new AesCryptoServiceProvider())
                    {
                        aesAlg.Key = Key.Key;
                        aesAlg.IV = Key.IV;
                        ICryptoTransform decryptor =
                            aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (var msDecrypt = new MemoryStream(encryptedBuffer))
                        using (var csDecrypt = new CryptoStream(
                            msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            csDecrypt.CopyTo(decryptedStream);
                            return decryptedStream.ToArray();
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                _logService.LogError($"{nameof(AesCryptographicService)} decrypt error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }

        public AesKey GenerateKey()
        {
            try
            {
                var aes = Aes.Create();
                return new AesKey { Key = aes.Key, IV = aes.IV };
            }
            catch (System.Exception e)
            {
                _logService.LogError($"{nameof(AesCryptographicService)} generate key error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }
    }
}
