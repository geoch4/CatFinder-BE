using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Users.DTOs
{
    public class UpdateAccountDto
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string? Username { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string? Email { get; set; }
    }
}
