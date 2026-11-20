using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.Models
{
    public class CategoryCreateViewModel
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public string CategoryName { get; set; }= string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal DistanceKM { get; set; }
    }
}