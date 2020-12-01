namespace PasswordManager.Models
{
    public interface IOperationResult
    {
        string ErrorMessage { get; }
        ResultType ResultType { get; }
    }
}