using GenshinApi.Dtos.WeaponDto;
using GenshinApi.Models;

namespace GenshinApi.Services.WeaponServices
{
    public interface IWeaponsService
    {
        Task<Weapons?> GetWeaponsById(int id);
        Task<IEnumerable<Weapons>> GetAllWeapons();
        Task<Weapons> CreateWeapon(WeaponCreationDto weapon);
        Task<Weapons> GetWeaponByName(string? name);
        Task<bool> GetNameExists(string? name);
        Task<bool> DeleteWeapon(int id);
        Task<bool> UpdateWeapon(int id, WeaponCreationDto weapon);
        Task<IEnumerable<Weapons>> GetAllWeaponsByWeaponType(int id);
        
    }
}
