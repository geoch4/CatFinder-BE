using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Cat
    {
        public int CatId { get; set; }

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

        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;

        public virtual ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}
