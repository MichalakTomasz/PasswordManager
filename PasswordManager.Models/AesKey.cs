using System;

namespace PasswordManager.Models
{
    [Serializable]
    public class AesKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
