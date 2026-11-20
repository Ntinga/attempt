using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs
{
    public class EventCreateDto
    {
        // Data Transfer Object for creating a new racing event (Microsoft, 2024)
        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(150, ErrorMessage = "Event name cannot exceed 150 characters.")]
        public string EventName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event date is required.")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Organiser ID is required.")]
        public int OrganiserId { get; set; }
        public string? BannerUrl { get; set; }
    }
}