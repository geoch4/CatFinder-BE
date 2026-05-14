using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Location.DTOs
{
    public class UpdateLocationDto
    {
        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Area { get; set; }

        [Range(-90.0, 90.0)]
        public decimal? Latitude { get; set; }

        [Range(-180.0, 180.0)]
        public decimal? Longitude { get; set; }
    }
}
