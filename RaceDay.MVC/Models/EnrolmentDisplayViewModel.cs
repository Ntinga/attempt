namespace RaceDay.MVC.Models
{
    public class EnrolmentDisplayViewModel
    {
        public string ParticipantName { get; set; }
            = string.Empty;

        public string EventName { get; set; }
            = string.Empty;

        public string CategoryName { get; set; }
            = string.Empty;

        public DateTime EnrolmentDate { get; set; }

        public string Status { get; set; }
            = "Confirmed";
    }
}