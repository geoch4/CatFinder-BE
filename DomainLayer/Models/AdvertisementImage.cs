using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class AdvertisementImage
    {
        public int AdvertisementImageId { get; set; }

        public int AdvertisementId { get; set; }

        [Required]
        [Url]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Advertisement Advertisement { get; set; } = null!;
    }
}
