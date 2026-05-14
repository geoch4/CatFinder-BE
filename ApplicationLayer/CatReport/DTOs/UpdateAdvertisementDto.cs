using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace ApplicationLayer.CatReport.DTOs
{
    public class UpdateAdvertisementDto
    {
        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? ContactPhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string? ContactEmail { get; set; }

        public DateTime? LastSeenAt { get; set; }

        public AdvertisementStatus? Status { get; set; }
    }
}
