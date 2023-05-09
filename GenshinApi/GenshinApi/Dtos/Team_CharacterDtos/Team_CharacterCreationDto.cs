using System.ComponentModel.DataAnnotations;

namespace GenshinApi.Dtos.Team_CharacterDtos
{
    public class Team_CharacterCreationDto
    {
        [Required(ErrorMessage = "Team Id must not be null")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Input is not accepted")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team Id must not be null")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Input is not accepted")]
        public int CharacterId { get; set; }
    }
}
