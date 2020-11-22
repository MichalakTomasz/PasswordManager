namespace PasswordManager.Models
{
    public class RegisterData : Credentials
    {
        public string SecondPassword { get; set; }
        public string SecondPasswordQuestion { get; set; }
    }
}
