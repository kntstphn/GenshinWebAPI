using System.ComponentModel.DataAnnotations;

namespace GenshinApi.Dtos.TeamCompositionDtos
{
    public class TeamCompositionCreationDto
    {
        [Required(ErrorMessage = "Team Composition Name is required")]
        public string Name { get; set; }
    }
}
