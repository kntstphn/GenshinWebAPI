using GenshinApi.Models;

namespace GenshinApi.Repositories.WeaponTypeRepositories
{
    public interface IWeaponTypeRepository
    {
        Task <IEnumerable<WeaponTypeWithList>> GetAllWeaponType();
        Task<WeaponTypeId> GetWeaponById(int wepId);
        Task<bool> GetId(int id);
        Task<WeaponType> GetTypeById(int id);
    }
}
