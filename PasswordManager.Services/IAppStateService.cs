namespace PasswordManager.Services
{
    public interface IAppStateService
    {
        bool IsInDebugMode { get; }
        bool IsInProductionMode { get; }
    }
}