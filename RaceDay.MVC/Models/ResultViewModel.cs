namespace RaceDay.MVC.Models
{
    public class ResultViewModel
    {
        public string EventName { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public TimeSpan FinishTime { get; set; }

        public int Position { get; set; }
    }
}