using System.ComponentModel.DataAnnotations;

namespace GenshinApi.Dtos.WeaponDto
{
    public class WeaponCreationUnderTypeDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The weapon name is required.")]
        [RegularExpression(@"^[A-Za-z\/\s\.'-]+$", ErrorMessage = "Characters not allowed")]
        [MaxLength(50, ErrorMessage = "The maximum length for the weapon name is 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The weapon damage is required.")]
        [Range(10, 50, ErrorMessage = "The lowest and highest weapon damage is 10 & 50, respectively.")]
        public int Damage { get; set; }

        [Required(ErrorMessage = "The weapon rarity is required.")]
        [Range(1, 5, ErrorMessage = "The lowest and highest weapon rarity is 1 & 5, respectively.")]
        public int Rarity { get; set; }
    }
}
