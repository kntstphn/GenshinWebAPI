using AutoMapper;
using GenshinApi.Dtos.WeaponDto;
using GenshinApi.Models;

namespace GenshinApi.Mappings
{
    public class WeaponMappings : Profile
    {
        public WeaponMappings()
        {
            CreateMap<WeaponCreationDto, Weapons>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.Damage, opt => opt.MapFrom(c => c.Damage))
                .ForMember(dto => dto.Rarity, opt => opt.MapFrom(c => c.Rarity));
            CreateMap<WeaponCreationUnderTypeDto, Weapons>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.Damage, opt => opt.MapFrom(c => c.Damage))
                .ForMember(dto => dto.Rarity, opt => opt.MapFrom(c => c.Rarity));

        }
    }
}
