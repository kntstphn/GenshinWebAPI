using GenshinApi.Models;
using GenshinApi.Repositories.WeaponTypeRepositories;

namespace GenshinApi.Services.WeaponTypeServices
{
    public class WeaponTypeService : IWeaponTypeService
    {
        private readonly IWeaponTypeRepository _wepTypeRep;
    

        public WeaponTypeService(IWeaponTypeRepository wepTypeRep)
        {
            _wepTypeRep = wepTypeRep;
            
        }

        public async Task<IEnumerable<WeaponTypeWithList>> GetAllWeaponType()
        {
            return await _wepTypeRep.GetAllWeaponType();
        }

        public async Task<bool> GetId(int id)
        {
            return await _wepTypeRep.GetId(id);
        }

        public async Task<WeaponTypeId> GetWeaponById(int wepId)
        {
            return await _wepTypeRep.GetWeaponById(wepId);
        }
        public async Task<WeaponType> GetTypeById(int id)
        {
            return await _wepTypeRep.GetTypeById(id);
        }

    }
}
