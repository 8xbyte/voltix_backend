namespace Voltix.Auth.Services
{
    public class PasswordHashService
    {
        private const int WorkFactor = 8;

        public bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }
    }
}

