using GenshinApi.Models;

namespace GenshinApi.Dtos.Team_CharacterDtos
{
    public class Team_CharacterCharacterIdDto
    {
        public string? Name { get; set; }
        public List<TeamCompositionNameDto> TeamCompositions { get; set; } = new List<TeamCompositionNameDto>();
    }
}
