using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManager.Services
{
    public class GenericCryptographicService : IGenericCryptographicService
    {
        public byte[] Encrypt(byte[] buffer)
        {
            var keyk = new List<byte>();
            var encryptedData = new List<byte>();
            for (var i = 0; i < buffer.Length; i++)
            {
                var key = GenateKey(buffer, i);
                keyk.Add(key);
                var encryptedElement = (byte)~(byte)(buffer[i] ^ key);
                encryptedData.Add(encryptedElement);
            }
            return encryptedData.ToArray();
        }

        public byte[] Decrypt(byte[] buffer)
        {
            var decryptedData = new List<byte>();
            for (var i = 0; i < buffer.Length; i++)
            {
                var key = GenateKey(buffer, i);
                var decryptedElement = (byte)((byte)~buffer[i] ^ key);
                decryptedData.Add(decryptedElement);
            }
            return decryptedData.ToArray();
        }

        byte GenateKey(byte[] buffer, int index)
        {
            var key = buffer.Length;
            while ((key -= index) > 255)
                key /= 2;
            return (byte)key;
        }
    }
}
