using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.Models
{
    public class EnrolmentCreateViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        public int ParticipantId { get; set; }
    }
}