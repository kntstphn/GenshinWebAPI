using AutoMapper;
using GenshinApi.Dtos.WeaponDto;
using GenshinApi.Models;
using GenshinApi.Repositories.WeaponRepositories;
using GenshinApi.Repositories.WeaponTypeRepositories;

namespace GenshinApi.Services.WeaponServices
{
    public class WeaponsService : IWeaponsService
    {
        private readonly IWeaponsRepository _weaponsRepository;
        private readonly IWeaponTypeRepository _wepTypeRepo;
        private readonly IMapper _mapper;

        public WeaponsService(IWeaponsRepository weaponsRepository, IWeaponTypeRepository wepTypeRepo, IMapper mapper)
        {
            _weaponsRepository = weaponsRepository;
            _wepTypeRepo = wepTypeRepo;
            _mapper = mapper;
        }

        public async Task<Weapons> CreateWeapon(WeaponCreationDto weaponDto)
        {
            var dtoToWep = _mapper.Map<Weapons>(weaponDto);
            dtoToWep.Type = await _wepTypeRepo.GetTypeById(weaponDto.WeaponType);
            return await _weaponsRepository.CreateWeapon(dtoToWep);
        }

        public async Task<bool> DeleteWeapon(int id)
        {
            return await _weaponsRepository.DeleteWeapon(id);
        }

        public async Task<IEnumerable<Weapons>> GetAllWeapons()
        {
            return await _weaponsRepository.GetAllWeapons();
        }

        public async Task<IEnumerable<Weapons>> GetAllWeaponsByWeaponType(int id)
        {
            return await _weaponsRepository.GetAllWeaponsByWeaponType(id);
        }

        public async Task<bool> GetNameExists(string? name)
        {
            return await _weaponsRepository.GetName(name);
        }

        public async Task<Weapons> GetWeaponByName(string? name)
        {
            return await _weaponsRepository.GetWeaponByName(name);
        }

        public async Task<Weapons?> GetWeaponsById(int id)
        {
            return await _weaponsRepository.GetWeapon(id);
        }

        public async Task<bool> UpdateWeapon(int id, WeaponCreationDto weapon)
        {
            var wepType = new WeaponType { Id = weapon.WeaponType };
            var wep = _mapper.Map<Weapons>(weapon);
            wep.Id = id;
            wep.Type = wepType;
            return await _weaponsRepository.UpdateWeapon(id, wep);
        }
    }
}
