using Domain.RepoContracts;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Infrastructure.RepoImps
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; 
        private const int HashSize = 32; 
        private const int Iterations = 210_000; 
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
        private readonly string _pepper;

        public PasswordHasher(IConfiguration configuration)
        {
           
            _pepper = configuration["Security:PasswordPepper"]
                      ?? throw new InvalidOperationException("Password Pepper is not configured.");
        }

        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            
            string pepperedPassword = password + _pepper;

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                pepperedPassword,
                salt,
                Iterations,
                Algorithm,
                HashSize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
                return false;

            var parts = passwordHash.Split('.', 2);
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] originalHash = Convert.FromBase64String(parts[1]);

            string pepperedPassword = password + _pepper;

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                pepperedPassword,
                salt,
                Iterations,
                Algorithm,
                HashSize);

            return CryptographicOperations.FixedTimeEquals(originalHash, inputHash);
        }
    }
}
