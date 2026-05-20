namespace ApplicationLayer.AdvertisementImages.DTOs
{
    public class AdvertisementImageResponseDto
    {
        public int AdvertisementImageId { get; set; }
        public int AdvertisementId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
