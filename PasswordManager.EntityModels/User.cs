using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.EntityModels
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] EncryptedPassword { get; set; }
        [Required]
        public byte[] EncryptedSecondPassword { get; set; }
        [Required]
        public byte[] EncryptedSecondPasswordQuestion { get; set; }
        [Required]
        public byte[] EncryptedKey { get; set; }
    }
}
