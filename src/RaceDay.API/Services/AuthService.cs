using Microsoft.AspNetCore.Identity;
using RaceDay.API.Models;

namespace RaceDay.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public string HashPassword(string password)
        {
            // Hash password using a secure default algorithm (PBKDF2) (Microsoft, 2024)
            return _passwordHasher.HashPassword(new User(), password);
        }

        public bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}