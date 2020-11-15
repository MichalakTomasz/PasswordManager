using Moq;
using PasswordManager.Services;
using Xunit;

namespace PasswordManager.Test
{
    public class DataBinaryConverterTest
    {
        [Fact]
        public void ShouldSerializedAndDeserializedTheSameValue()
        {
            var text = "ThisIsTest";
            var logServiceMock = new Mock<ILogService>();
            var appStateServiceMock = new Mock<IAppStateService>();
            var dataBinaryService = new DataBinarySerializeService(logServiceMock.Object, appStateServiceMock.Object);
            var serialized = dataBinaryService.Serialize<string>(text);
            var deserialized = dataBinaryService.Deserialize<string>(serialized);
            
            Assert.Equal(text, deserialized);
        }
    }
}
