using System.ComponentModel.DataAnnotations;

namespace GenshinApi.Dtos.CharDto
{
    public class CharacterCreationDto
    {
        [Required(ErrorMessage = "Character Name is required")]
        [RegularExpression(@"^[A-Za-z\/\s\.'-]+$", ErrorMessage = "Characters not allowed")]
        [MaxLength(50, ErrorMessage = "Maximum length for Character Name is 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Character Rarity is required")]
        public string? Rarity { get; set; }

        [Required(ErrorMessage = "Character Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Character WeaponId is required")]
        [Range(1,int.MaxValue, ErrorMessage = "WeaponID must be at least 1")]
        public int WeaponId { get; set; }

        [Required(ErrorMessage = "Character RegionId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "RegionId must be at least 1")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Character SetId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "SetId must be at least 1")]
        public int SetId { get; set; }

        [Required(ErrorMessage = "Character ElemId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ELemId must be at least 1")]
        public int ElemId { get; set; }
    }
}
