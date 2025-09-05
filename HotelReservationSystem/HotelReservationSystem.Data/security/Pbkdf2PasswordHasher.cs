using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Infrastructure.Security
{
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100000;
        public string HashPassword(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);

                var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
                var hash = key.GetBytes(KeySize);

                var result = new byte[SaltSize + KeySize];
                Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
                Buffer.BlockCopy(hash, 0, result, SaltSize, KeySize);

                return Convert.ToBase64String(result);
            }
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            var hashBytes = Convert.FromBase64String(storedHash);

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

            var storedKey = new byte[KeySize];
            Buffer.BlockCopy(hashBytes, SaltSize, storedKey, 0, KeySize);

            var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var keyToCheck = key.GetBytes(KeySize);

            if (storedKey.Length != keyToCheck.Length) return false;
            int diff = 0;
            for (int i = 0; i < storedKey.Length; i++)
            {
                diff |= storedKey[i] ^ keyToCheck[i];
            }
            return diff == 0;
        }
    }
}
