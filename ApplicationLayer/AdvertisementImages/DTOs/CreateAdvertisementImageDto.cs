using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.AdvertisementImages.DTOs
{
    public class CreateAdvertisementImageDto
    {
        [Required]
        public int AdvertisementId { get; set; }

        [Required]
        [Url]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }
    }
}
