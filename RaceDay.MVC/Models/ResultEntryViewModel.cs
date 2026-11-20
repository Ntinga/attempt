using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.Models
{
    public class ResultEntryViewModel
    {
        [Required]
        public int EnrolmentId { get; set; }

        [Required]
        public TimeSpan FinishTime { get; set; }

        [Required]
        public int Position { get; set; }
    }
}