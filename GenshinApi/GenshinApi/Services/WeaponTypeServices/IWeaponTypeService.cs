using GenshinApi.Models;

namespace GenshinApi.Services.WeaponTypeServices
{
    public interface IWeaponTypeService
    {
        Task<IEnumerable<WeaponTypeWithList>> GetAllWeaponType();
        Task<WeaponTypeId> GetWeaponById(int wepId);
        Task<bool> GetId(int id);
        Task<WeaponType> GetTypeById(int id);

    }
}
