using System;

namespace PasswordManager.Services
{
    public interface ILogService
    {
        void LogDebug(string message);
        void LogError(Exception e);
        void LogError(string message);
        void LogMessage(string message);
        bool IsLoggerEnabled { get; set; }
    }
}