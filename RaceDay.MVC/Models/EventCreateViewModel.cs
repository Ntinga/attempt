using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RaceDay.MVC.Models
{
    public class EventCreateViewModel
    {
        [Required]
        public string EventName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public int OrganiserId { get; set; }
        public string? BannerUrl { get; set; }
        public IFormFile? BannerImage { get; set; }
    }
}