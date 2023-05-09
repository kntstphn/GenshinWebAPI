using GenshinApi.Dtos.WeaponDto;
using GenshinApi.Models;

namespace GenshinApi.Services.WeaponServices
{
    public interface ICreateWeaponUnderTypeService
    {
        Task<Weapons> CreateWeaponUnderType(int weaponId, WeaponCreationUnderTypeDto weapon);
    }
}
