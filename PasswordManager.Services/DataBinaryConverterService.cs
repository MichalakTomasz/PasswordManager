using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PasswordManager.Services
{
    public class DataBinaryConverterService : IDataBinaryConverterService
    {
        private readonly ILogService _logService;
        private readonly IAppStateService _appStateService;

        public DataBinaryConverterService(ILogService logService, IAppStateService appStateService)
        {
            _logService = logService;
            _appStateService = appStateService;
        }

        public byte[] Serialize<TData>(TData password)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, password);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception e)
            {
                _logService.LogError($"{nameof(DataBinaryConverterService)} error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }

        public TData Deserialize<TData>(byte[] buffer)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream(buffer))
                {
                    return (TData)binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (Exception e)
            {
                _logService.LogError($"{nameof(DataBinaryConverterService)} error: {e.Message}");
                if (_appStateService.IsInDebugMode)
                    throw;
                else
                    return default;
            }
        }
    }
}
