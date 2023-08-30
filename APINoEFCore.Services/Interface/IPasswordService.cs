namespace APINoEFCore.Services.Interface
{
    public interface IPasswordService
    {
        (string Hash, string Salt) HashPassword(string password);
        bool VerifyPassword(string password, string hash, string salt);
        bool IsPasswordValid(string enteredPassword, string storedHash, string storedSalt);
    }
}