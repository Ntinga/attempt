namespace RaceDay.API.Models
{
    public class Enrolment
    {
        public int EnrolmentId { get; set; }
        public int CategoryId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime EnrolmentDate { get; set; } = DateTime.UtcNow;

        // Navigation properties (Microsoft, 2024)
        public Category? Category { get; set; }
        public User? Participant { get; set; }
        public Result? Result { get; set; }
    }
}