namespace RaceDay.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Hashed password storage following security best practices (Microsoft, 2024)
        public string PasswordHash { get; set; } = string.Empty;

        // Foreign key mapping to Role entity
        public int RoleId { get; set; }

        // Navigation properties for EF Core relationship resolution (Microsoft, 2024)
        public Role? Role { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Event> OrganisedEvents { get; set; } = new List<Event>();
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}