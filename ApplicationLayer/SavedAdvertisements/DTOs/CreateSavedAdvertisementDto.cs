using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.SavedAdvertisements.DTOs
{
    public class CreateSavedAdvertisementDto
    {
        [Required]
        public int AdvertisementId { get; set; }
    }
}
