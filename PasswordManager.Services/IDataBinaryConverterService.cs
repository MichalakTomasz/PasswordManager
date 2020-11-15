namespace PasswordManager.Services
{
    public interface IDataBinaryConverterService
    {
        TData Deserialize<TData>(byte[] buffer);
        byte[] Serialize<TData>(TData password);
    }
}