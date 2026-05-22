using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public int AdvertisementId { get; set; }

        public int AccountId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; } = string.Empty;

        public int? CommentId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Advertisement Advertisement { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}
