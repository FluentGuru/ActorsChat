using ActorsChat.Domain.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ActorsChat.Infrastructure.Services
{
    public class Sha1Hasher : IHasher
    {
        public string Generate(string source, string salt)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
                return Convert.ToBase64String(hash);
            }
        }

        public string GetSalt(int length)
        {
            var salt = "";
            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                salt += (char)random.Next(64, 90);
            }

            return salt;
        }
    }
}
