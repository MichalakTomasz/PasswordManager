using System;

namespace PasswordManager.Services
{
    public class LogService : ILogService
    {
        public bool IsLoggerEnabled { get; set; }

        public void LogError(Exception e)
        {

        }
        public void LogError(string message)
        {

        }

        public void LogMessage(string message)
        {

        }

        public void LogDebug(string message)
        {

        }
    }
}
