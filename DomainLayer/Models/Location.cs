using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Area { get; set; }

        [Range(-90.0, 90.0)]
        public decimal? Latitude { get; set; }

        [Range(-180.0, 180.0)]
        public decimal? Longitude { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}
