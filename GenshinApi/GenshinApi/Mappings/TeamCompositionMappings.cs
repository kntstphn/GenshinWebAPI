using AutoMapper;

namespace GenshinApi.Mappings
{
    using AutoMapper;
    using GenshinApi.Dtos.TeamCompositionDtos;
    using GenshinApi.Models;

    public class TeamCompositionMappings : Profile
    {
        public TeamCompositionMappings()
        {
            CreateMap<TeamComposition, TeamCompositionDto>();
        }
    }
}
