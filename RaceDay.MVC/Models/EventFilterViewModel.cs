namespace RaceDay.MVC.Models
{
    public class EventFilterViewModel
    {
        public string? SearchTerm { get; set; }

        public DateTime? EventDate { get; set; }

        public decimal? DistanceKM { get; set; }
    }
}