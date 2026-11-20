namespace RaceDay.API.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;

        // Foreign key mapping to Organiser (User entity)
        public int OrganiserId { get; set; }

        // Navigation properties for EF Core (Microsoft, 2024)
        public User? Organiser { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public string? BannerUrl { get; set; }
    }
}