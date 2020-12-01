namespace PasswordManager.Models
{
    public class RegisterModel : Credentials
    {
        public string SecondPassword { get; set; }
        public string SecondPasswordQuestion { get; set; }
    }
}
