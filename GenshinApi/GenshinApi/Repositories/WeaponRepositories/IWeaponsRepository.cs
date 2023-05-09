using GenshinApi.Models;

namespace GenshinApi.Repositories.WeaponRepositories
{
    public interface IWeaponsRepository
    {
        Task<Weapons> CreateWeapon(Weapons weapon);
        Task<bool> DeleteWeapon(int id);
        Task<bool> UpdateWeapon(int id, Weapons weapon);
        Task<Weapons?> GetWeapon(int id);
        Task<Weapons> GetWeaponByName(string? name);
        Task<IEnumerable<Weapons>> GetAllWeapons();
        Task<bool> GetName(string? name);
        Task<IEnumerable<Weapons>> GetAllWeaponsByWeaponType(int Id);
    }
}
