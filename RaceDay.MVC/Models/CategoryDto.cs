namespace RaceDay.MVC.Models
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public int EventId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal DistanceKM { get; set; }
    }
}