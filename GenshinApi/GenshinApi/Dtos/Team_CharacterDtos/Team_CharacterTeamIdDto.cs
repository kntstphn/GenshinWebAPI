using GenshinApi.Models;

namespace GenshinApi.Dtos.Team_CharacterDtos
{
    public class Team_CharacterTeamIdDto
    {
        public string TeamComp { get; set; }
        public List<CharacterNameDto>? Characters { get; set; } = new List<CharacterNameDto>();
    }
}
