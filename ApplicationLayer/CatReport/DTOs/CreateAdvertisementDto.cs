using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace ApplicationLayer.CatReport.DTOs
{
    public class CreateAdvertisementDto
    {
        // Cat fields — a new cat is created together with the advertisement
        [MaxLength(100)]
        public string? CatName { get; set; }

        [MaxLength(100)]
        public string? Breed { get; set; }

        [Required]
        [MaxLength(100)]
        public string FurColor { get; set; } = string.Empty;

        // Location fields — a new location is created together with the advertisement
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Area { get; set; }

        // Advertisement fields
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
