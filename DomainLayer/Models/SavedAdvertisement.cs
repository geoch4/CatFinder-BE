namespace DomainLayer.Models
{
    public class SavedAdvertisement
    {
        public int SavedAdvertisementId { get; set; }

        public int AccountId { get; set; }

        public int AdvertisementId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Account Account { get; set; } = null!;
        public virtual Advertisement Advertisement { get; set; } = null!;
    }
}
