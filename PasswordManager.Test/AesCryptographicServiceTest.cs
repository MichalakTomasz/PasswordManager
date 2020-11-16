using Moq;
using PasswordManager.Services;
using Xunit;

namespace PasswordManager.Test
{
    public class AesCryptographicServiceTest
    {
        [Fact]
        public void ShouldEncryptAndDecryptData()
        {
            var logServiceMock = new Mock<ILogService>();
            var appStateServiceMock = new Mock<IAppStateService>();
            var aesCryptographicService = new AesCryptographicService(logServiceMock.Object, appStateServiceMock.Object);
            var key = aesCryptographicService.GenerateKey();
            aesCryptographicService.Key = key;
            var text = "ThisIsTest";
            var dataBinarySerializeService = new DataBinarySerializeService(logServiceMock.Object, appStateServiceMock.Object);
            var serialized = dataBinarySerializeService.Serialize<string>(text);
            var encrypted = aesCryptographicService.Encrypt(serialized);
            var decrypted = aesCryptographicService.Decrypt(encrypted);
            var deserialized = dataBinarySerializeService.Deserialize<string>(decrypted);

            Assert.Equal(text, deserialized);
        }
    }
}
