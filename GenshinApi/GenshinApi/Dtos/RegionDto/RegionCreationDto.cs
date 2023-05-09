using System.ComponentModel.DataAnnotations;

namespace GenshinApi.Dtos.RegionDto
{
    public class RegionCreationDto
    {
        [Required(ErrorMessage = "The Region name is required.")]
        [MaxLength(30, ErrorMessage = "Max lenghth for the name of the Region is 30 characters.")]
        public string? Name { get; set; }
        public string? RegionInspiredFrom { get; set; }
        public string? RegionDescription { get; set; }
    }
}
