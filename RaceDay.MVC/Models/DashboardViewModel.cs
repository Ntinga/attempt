namespace RaceDay.MVC.Models
{
    public class DashboardViewModel
    {
        public int TotalEvents { get; set; }

        public int TotalEnrolments { get; set; }

        public List<EventDto> UpcomingEvents { get; set; }
            = new();
    }
}