namespace RaceDay.API.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Navigation property for 1-to-Many relationship with Users (Microsoft, 2024)
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}