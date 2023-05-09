using AutoMapper;

namespace GenshinApi.Mappings
{
    using AutoMapper;
    using GenshinApi.Dtos.ArtifactSetDtos;
    using GenshinApi.Models;

    public class ArtifactSetMappings : Profile
    { 
        public ArtifactSetMappings ()
        {
            CreateMap<ArtifactSet, ArtifactSetDto>();
        }
    }
}
