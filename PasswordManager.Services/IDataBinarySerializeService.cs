namespace PasswordManager.Services
{
    public interface IDataBinarySerializeService
    {
        TData Deserialize<TData>(byte[] buffer);
        byte[] Serialize<TData>(TData sourceData);
    }
}