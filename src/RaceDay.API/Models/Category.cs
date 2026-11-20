namespace RaceDay.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int EventId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DistanceKM { get; set; }

        // Navigation properties (Microsoft, 2024)
        public Event? Event { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}