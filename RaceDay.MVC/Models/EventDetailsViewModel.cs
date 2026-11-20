namespace RaceDay.MVC.Models
{
    public class EventDetailsViewModel
    {
        public EventDto Event { get; set; } = new();

        public List<CategoryDto> Categories { get; set; }
            = new();
    }
}