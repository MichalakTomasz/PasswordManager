using System.ComponentModel.DataAnnotations;

namespace PasswordManager.EntityModels
{
    public class PasswordSet
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string EncryptedPassword { get; set; }
        public string Comment { get; set; }
        [Required]
        public User User { get; set; }
    }
}
