using API.Entities;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    public class PasswordHasher : IPasswordHasher<AppUser>
    {
        public string HashPassword(AppUser user, string password)
        {
            // Generate a new salt for each user
            var salt = GenerateSalt();
            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing, // Argon2id
                Version = Argon2Version.Nineteen,
                TimeCost = 4,
                MemoryCost = 65536,
                Lanes = 4,
                Threads = Environment.ProcessorCount,
                Password = Encoding.UTF8.GetBytes(password),
                Salt = Convert.FromBase64String(salt),
                HashLength = 32
            };

            using var hasher = new Argon2(config);
            using var hash = hasher.Hash();

            var encodedHash = config.EncodeString(hash.Buffer);

            // Combine salt and hash into one string to store (delimited)
            return $"{salt}${encodedHash}";
        }

        public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
        {
            try
            {
                // Split salt and hash from the stored value
                var parts = hashedPassword.Split('$');
                if (parts.Length != 2)
                    return PasswordVerificationResult.Failed;

                var salt = parts[0];
                var encodedHash = parts[1];

                // Rehash the provided password using the same salt
                var config = new Argon2Config
                {
                    Type = Argon2Type.HybridAddressing,
                    Version = Argon2Version.Nineteen,
                    TimeCost = 4,
                    MemoryCost = 65536,
                    Lanes = 4,
                    Threads = Environment.ProcessorCount,
                    Password = Encoding.UTF8.GetBytes(providedPassword),
                    Salt = Convert.FromBase64String(salt),
                    HashLength = 32
                };

                using var hasher = new Argon2(config);
                using var hash = hasher.Hash();

                var reEncodedHash = config.EncodeString(hash.Buffer);

                if (reEncodedHash == encodedHash)
                    return PasswordVerificationResult.Success;
                else
                    return PasswordVerificationResult.Failed;
            }
            catch
            {
                return PasswordVerificationResult.Failed;
            }
        }

        private static string GenerateSalt(int size = 16)
        {
            var saltBytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}
