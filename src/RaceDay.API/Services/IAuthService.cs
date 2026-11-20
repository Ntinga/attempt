namespace RaceDay.API.Services
{
    public interface IAuthService
    {
        // Contract for authentication and password management operations (Microsoft, 2024)
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string hashedPassword);
    }
}