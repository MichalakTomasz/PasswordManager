namespace PasswordManager.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
        public string EncryptedKey { get; set; }
    }
}
