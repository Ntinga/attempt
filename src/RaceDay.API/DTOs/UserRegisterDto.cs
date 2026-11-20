using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs
{
    public class UserRegisterDto
    {
        // Data Transfer Object for incoming user registration requests (Microsoft, 2024)
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role ID is required.")]
        public int RoleId { get; set; }
    }
}