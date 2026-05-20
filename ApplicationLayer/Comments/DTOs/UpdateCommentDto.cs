using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Comments.DTOs
{
    public class UpdateCommentDto
    {
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; } = string.Empty;
    }
}
