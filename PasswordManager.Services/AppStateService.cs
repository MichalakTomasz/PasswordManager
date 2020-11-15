namespace PasswordManager.Services
{
    public class AppStateService : IAppStateService
    {
        public bool IsInDebugMode
        {
            get
            {
                var result = false;
#if DEBUG
                result = true;
#endif
                return result;
            }
        }

        public bool IsInProductionMode
        {
            get
            {
                var result = false;
#if !DEBUG
                result = true;
#endif
                return result;
            }
        }
    }
}
