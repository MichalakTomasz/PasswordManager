using PasswordManager.EntityModels;
using PasswordManager.Models;
using System;

namespace PasswordManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataService _dataService;
        private readonly IAppStateService _appStateService;
        private readonly ILogService _logService;
        private readonly IAesCryptographicService _aesCryptographicService;
        private readonly IGenericCryptographicService _genericCryptographicService;
        private readonly IDataBinarySerializeService _dataBinarySerializeService;

        public User LoggedUser { get; private set; }
        public bool IsLogged { get; private set; }
        public AccountService(IDataService dataService, IAppStateService appStateService, 
            ILogService logService, IAesCryptographicService aesCryptographicService,
            IGenericCryptographicService genericCryptographicService,
            IDataBinarySerializeService dataBinarySerializeService)
        {
            _dataService = dataService;
            _appStateService = appStateService;
            _logService = logService;
            _aesCryptographicService = aesCryptographicService;
            _genericCryptographicService = genericCryptographicService;
            _dataBinarySerializeService = dataBinarySerializeService;

            IsLogged = false;
        }
        public bool Login(Credentials credentials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(credentials?.Login) ||
                    string.IsNullOrWhiteSpace(credentials.Password))
                    throw new ArgumentNullException();

                var user = _dataService.GetUser(credentials.Login);
                var result = false;
                if (user != null)
                {
                    var decryptedUserKeyByteBuffer = _genericCryptographicService.Decrypt(user.EncryptedKey);
                    var userKey = _dataBinarySerializeService.Deserialize<AesKey>(decryptedUserKeyByteBuffer);
                    _aesCryptographicService.Key = userKey;
                    var decryptedPasswordByteBuffer =_aesCryptographicService.Decrypt(user.EncryptedPassword);
                    var password = _dataBinarySerializeService.Deserialize<string>(decryptedPasswordByteBuffer);
                    result = credentials.Password == password;
                }

                if (result)
                {
                    LoggedUser = user;
                    IsLogged = true;
                    return true;
                }
                else
                {
                    LoggedUser = null;
                    IsLogged = false;
                    return false;
                }
            }
            catch (Exception e)
            {
                _logService.LogError($"Account service Login error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return false;
            }
        }

        public void Logout()
        {
            LoggedUser = null;
            IsLogged = false;
        }

        public void Register(RegisterData registerData)
        {
            try
            {
                var userKey = _aesCryptographicService.GenerateKey();
                _aesCryptographicService.Key = userKey;
                var userKeyByteBuffer = _dataBinarySerializeService.Serialize<AesKey>(userKey);
                var encryptedUserKey = _genericCryptographicService.Encrypt(userKeyByteBuffer);

                var passwordByteBuffer = _dataBinarySerializeService.Serialize<string>(registerData.Password);
                var encryptedPassword = _aesCryptographicService.Encrypt(passwordByteBuffer);
                var secondPasswordByteBuffer = _dataBinarySerializeService.Serialize<string>(registerData.SecondPassword);
                var encryptedSecondPassword = _aesCryptographicService.Encrypt(secondPasswordByteBuffer);
                var secondPasswordQuestionByteBuffer = _dataBinarySerializeService.Serialize<string>(registerData.SecondPasswordQuestion);
                var encryptedSecondPasswordQuestion = _aesCryptographicService.Encrypt(secondPasswordQuestionByteBuffer);

                var newUser = new User
                {
                    Username = registerData.Login.Trim().ToUpper(),
                    EncryptedKey = encryptedUserKey,
                    EncryptedPassword = encryptedPassword,
                    EncryptedSecondPassword = encryptedSecondPassword,
                    EncryptedSecondPasswordQuestion = encryptedSecondPasswordQuestion
                };
                _dataService.SaveUser(newUser);
            }
            catch (Exception e)
            {
                _logService.LogError($"{nameof(AccountService)} {nameof(AccountService.Register)} error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
            }
        }

        public bool RecoverPassword(string username)
        {
            throw new NotImplementedException();
        }
    }
}
