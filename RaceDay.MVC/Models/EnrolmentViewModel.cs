namespace RaceDay.MVC.Models
{
    public class EnrolmentViewModel
    {
        public int EnrolmentId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public DateTime EnrolmentDate { get; set; }

        public string Status { get; set; } = "Confirmed";
    }
}