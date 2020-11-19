namespace PasswordManager.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public byte[] EncryptedKey { get; set; }
    }
}
