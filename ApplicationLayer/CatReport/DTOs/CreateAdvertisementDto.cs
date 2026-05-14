using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace ApplicationLayer.CatReport.DTOs
{
    public class CreateAdvertisementDto
    {
        [Required]
        public int CatId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Phone]
        [MaxLength(20)]
        public string? ContactPhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string? ContactEmail { get; set; }

        public DateTime LastSeenAt { get; set; }

        [Required]
        public AdvertisementType Type { get; set; }
    }
}
