﻿namespace PasswordManager.EntityModels
{
    public class PasswordSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}
