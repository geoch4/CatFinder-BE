using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public int AdvertisementId { get; set; }

        public int AccountId { get; set; }

        public int? ParentCommentId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Body { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public virtual Advertisement Advertisement { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}
