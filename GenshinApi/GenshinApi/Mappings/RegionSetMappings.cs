using AutoMapper;
using GenshinApi.Dtos.RegionDto;
using GenshinApi.Models;

namespace GenshinApi.Mappings
{
   

    public class RegionSetMappings : Profile
    {
        public RegionSetMappings()
        {
            CreateMap<Region, RegionDto>();
        }
    }
}