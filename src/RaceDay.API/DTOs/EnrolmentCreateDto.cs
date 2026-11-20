using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs
{
    public class EnrolmentCreateDto
    {
        // Data Transfer Object for creating a new event enrolment (Microsoft, 2024)
        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Participant ID is required.")]
        public int ParticipantId { get; set; }
    }
}