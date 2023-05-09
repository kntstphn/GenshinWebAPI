using System.ComponentModel.DataAnnotations;

namespace GenshinApi.Dtos.ArtifactSetDtos
{
    public class ArtifactSetCreationDto
    {
        [Required(ErrorMessage = "Artifact Set Name is required")]
        [RegularExpression(@"^[A-Za-z\/\s\.'-]+$", ErrorMessage = "Characters not allowed")]
        [MaxLength(99, ErrorMessage = "Maximum length for Artifact Set Name is 99 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Artifact Set Description is required")]
        [MaxLength(999, ErrorMessage = "Maximum length for Artifact Set Description is 99 characters")]
        public string? Description { get; set; }
    }
}
