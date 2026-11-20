namespace RaceDay.API.DTOs
{
    public class ResultCreateDto
    {
        // Data Transfer Object for recording a new event result (Microsoft, 2024)
        public int EnrolmentId { get; set; }
        public TimeSpan FinishTime { get; set; }
        public int Position { get; set; }
    }
}