using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs
{
    public class CategoryCreateDto
    {
        // Data Transfer Object for creating a new race category (Microsoft, 2024)
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event ID is required.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Distance in KM is required.")]
        public decimal DistanceKM { get; set; }
    }
}