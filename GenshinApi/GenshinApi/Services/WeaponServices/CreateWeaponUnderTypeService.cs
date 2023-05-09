using AutoMapper;
using GenshinApi.Dtos.WeaponDto;
using GenshinApi.Models;
using GenshinApi.Repositories.WeaponRepositories;
using GenshinApi.Repositories.WeaponTypeRepositories;

namespace GenshinApi.Services.WeaponServices
{
    public class CreateWeaponUnderTypeService : ICreateWeaponUnderTypeService
    {
        private readonly IWeaponsRepository _weaponsRepository;
        private readonly IWeaponTypeRepository _wepTypeRepo;
        private readonly IMapper _mapper;

        public CreateWeaponUnderTypeService(IWeaponsRepository weaponsRepository, IWeaponTypeRepository wepTypeRepo, IMapper mapper)
        {
            _weaponsRepository = weaponsRepository;
            _wepTypeRepo = wepTypeRepo;
            _mapper = mapper;
        }
        public async Task<Weapons> CreateWeaponUnderType(int weaponId, WeaponCreationUnderTypeDto weapon)
        {
            // Convert DTO -> Model
            var model = _mapper.Map<Weapons>(weapon);
            model.Type = await _wepTypeRepo.GetTypeById(weaponId);
            return  await _weaponsRepository.CreateWeapon(model);
        }
    }
}
