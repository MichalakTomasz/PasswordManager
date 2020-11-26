using System.ComponentModel.DataAnnotations;

namespace PasswordManager.EntityModels
{
    public class PasswordSet
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Username { get; set; }
        [Required]
        public byte[] EncryptedPassword { get; set; }
        public string Comment { get; set; }
        [Required]
        public User User { get; set; }
    }
}
