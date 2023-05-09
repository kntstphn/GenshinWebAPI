using GenshinApi.Models;
using AutoMapper;
using GenshinApi.Dtos.CharDto;

namespace GenshinApi.Mappings
{
    public class CharacterMappings : Profile
    {
        public CharacterMappings()
        {
            CreateMap<Character, CharacterDto>()    
                .ForMember(dto => dto.Weapon, opt => opt.MapFrom(c => c.Weapon.Name))
                .ForMember(dto => dto.Region, opt => opt.MapFrom(c => c.Region.Name))
                .ForMember(dto => dto.Set, opt => opt.MapFrom(c => c.ArtifactSet.Name))
                .ForMember(dto => dto.Element, opt => opt.MapFrom(c => c.Element.Name));

            CreateMap<CharacterDto, CharacterCreationDto>();
        }
        
    }
}
