using GenshinApi.Dtos.Team_CharacterDtos;
using GenshinApi.Models;
using AutoMapper;
namespace GenshinApi.Mappings
{
    
    public class Team_CharacterMappings : Profile
    {
        public Team_CharacterMappings()
        {
            CreateMap<Team_Character, Team_CharacterDto>()
                .ForMember(dto => dto.TeamName, opt => opt.MapFrom(opt => opt.TeamComp.Name))
                .ForMember(dto => dto.CharacterName, opt => opt.MapFrom(opt => opt.Char.Name));

            CreateMap<TeamComposition, TeamCompositionNameDto>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(opt => opt.Name));

            CreateMap<TeamByTeamId, Team_CharacterTeamIdDto>()
                .ForMember(dto => dto.TeamComp, opt => opt.MapFrom(opt => opt.TeamComp));

            CreateMap<TeamByCharacterId, Team_CharacterCharacterIdDto>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(opt => opt.Char));

            CreateMap<Character, CharacterNameDto>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(opt => opt.Name));
        }
    }
}
