using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Comments.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        public int AdvertisementId { get; set; }

        public int? ParentCommentId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Body { get; set; } = string.Empty;
    }
}
