using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Reports.DTOs
{
    public class CreateReportDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Comment { get; set; } = string.Empty;
    }
}
