namespace RaceDay.API.DTOs
{
    public class UserLoginDto
    {
        // Data Transfer Object for incoming user login credentials (Microsoft, 2024)
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}