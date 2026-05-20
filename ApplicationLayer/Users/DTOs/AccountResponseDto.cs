using DomainLayer.Models;

namespace ApplicationLayer.Users.DTOs
{
    public class AccountResponseDto
    {
        public int AccountId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
