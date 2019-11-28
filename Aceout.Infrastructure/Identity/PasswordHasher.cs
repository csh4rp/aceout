using Aceout.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Aceout.Infrastructure.DataModel.Identity;
using Aceout.Domain.Model.Identity;

namespace Aceout.Infrastructure.Identity
{
    public class PasswordHasher : IPasswordHasher<User>
    {
        private const int KeyLength = 128;

        public string HashPassword(User user, string password)
        {
            return Hash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            var bytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = new byte[bytes.Length - 64];

            for (int i = 0; i < saltBytes.Length; i++)
            {
                saltBytes[i] = bytes[64 + i];
            }

            var providedBytes = new List<byte>();
            providedBytes.AddRange(Encoding.UTF8.GetBytes(providedPassword));
            providedBytes.AddRange(saltBytes);

            byte[] hash;
            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(providedBytes.ToArray());
            }

            var passwordHash = new byte[hash.Length + saltBytes.Length];
            hash.CopyTo(passwordHash, 0);
            saltBytes.CopyTo(passwordHash, hash.Length);

            var providedPasswordHash = Convert.ToBase64String(passwordHash);

            if (hashedPassword.Equals(providedPasswordHash))
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }

        private string Hash(string password)
        {
            var builder = new StringBuilder();
            byte[] hash;

            var saltBytes = GenerateRandomBytes(KeyLength);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var allBytes = new List<byte>(passwordBytes);
            allBytes.AddRange(saltBytes);

            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(allBytes.ToArray());
            }

            var passwordHash = new byte[hash.Length + saltBytes.Length];
            hash.CopyTo(passwordHash, 0);
            saltBytes.CopyTo(passwordHash, hash.Length);

            return Convert.ToBase64String(passwordHash);
        }

        private byte[] GenerateRandomBytes(int keyLength)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[keyLength];
                rng.GetBytes(bytes);
                return bytes;
            }
        }
    }
}
