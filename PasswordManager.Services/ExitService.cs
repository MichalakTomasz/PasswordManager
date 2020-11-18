using System;

namespace PasswordManager.Services
{
    public class ExitService : IExitService
    {
        public void AppExit()
            => Environment.Exit(0);
    }
}
