namespace ApplicationLayer.Location.DTOs
{
    public class LocationResponseDto
    {
        public int LocationId { get; set; }
        public string City { get; set; } = string.Empty;
        public string? Area { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
