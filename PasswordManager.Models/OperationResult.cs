namespace PasswordManager.Models
{
    public class OperationResult : IOperationResult
    {
        public OperationResult(ResultType resultType, string errorMessage)
        {
            ResultType = resultType;
            ErrorMessage = errorMessage;
        }
        public OperationResult(ResultType resultType)
        {
            ResultType = ResultType;
            ErrorMessage = "";
        }

        public ResultType ResultType { get; }
        public string ErrorMessage { get; }
    }
}
