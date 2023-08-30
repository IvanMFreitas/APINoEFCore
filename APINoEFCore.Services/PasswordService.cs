using System.Security.Cryptography;
using System.Text;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class PasswordService : IPasswordService
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[32];
                rng.GetBytes(saltBytes);

                using (var sha256 = SHA256.Create())
                {
                    byte[] saltedPassword = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(saltBytes));
                    byte[] hashBytes = sha256.ComputeHash(saltedPassword);

                    string hash = Convert.ToBase64String(hashBytes);
                    string salt = Convert.ToBase64String(saltBytes);

                    return (hash, salt);
                }
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(enteredPassword + storedSalt);
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                string computedHash = Convert.ToBase64String(hashBytes);

                return computedHash == storedHash;
            }
        }

        public bool IsPasswordValid(string enteredPassword, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(enteredPassword) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
            {
                return false;
            }

            return VerifyPassword(enteredPassword, storedHash, storedSalt);
        }
    }
}

