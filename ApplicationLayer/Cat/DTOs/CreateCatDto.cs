using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace ApplicationLayer.Cat.DTOs
{
    public class CreateCatDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Breed { get; set; }

        [Required]
        [MaxLength(100)]
        public string FurColor { get; set; } = string.Empty;

        public int? Age { get; set; }

        public CatGender? Gender { get; set; }

        public bool IsChipped { get; set; }

        public bool IsRegistered { get; set; }

        [MaxLength(50)]
        public string? ChipNumber { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}
