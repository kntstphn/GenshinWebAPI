using AutoMapper;
using GenshinApi.Dtos.CharDto;
using GenshinApi.Models;
using GenshinApi.Repositories.Characters;

namespace GenshinApi.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _repository;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }

        public async Task<CharacterDto> CreateChar(CharacterCreationDto charToCreate)
        {
            //Convert dto to Models
            var charModel = new Character
            {
                Name = charToCreate.Name,
                Rarity = charToCreate.Rarity,
                Gender = charToCreate.Gender,
                Weapon = await _repository.GetWeapon(charToCreate.WeaponId),
                Region = await _repository.GetRegion(charToCreate.RegionId),
                ArtifactSet = await _repository.GetArtifactSet(charToCreate.SetId),
                Element = await _repository.GetElement(charToCreate.ElemId)
            };

            charModel.Id = await _repository.CreateNewChar(charModel);
            return _mapper.Map<CharacterDto>(charModel);


        }

        public async Task<bool> DeleteChar(int id)
        {
            return await _repository.DeleteChar(id);
        }

        public async Task<IEnumerable<CharacterDto>> GetAllChar()
        {
            var charModels = await _repository.GetAll();

            return _mapper.Map<IEnumerable<CharacterDto>>(charModels);
        }

        public async Task<CharacterDto> GetCharById(int id)
        {
            var model = await _repository.GetCharById(id);
            if(model == null)
            {
                return null;
            }
            return _mapper.Map<CharacterDto>(model);
        }
        public async Task<IEnumerable<CharacterDto>> GetAllCharByElementName(string elementName)
        {
            var characterModels = await _repository.GetAllByElementName(elementName);

            return _mapper.Map<IEnumerable<CharacterDto>>(characterModels);
        }


        public async Task<bool> UpdateChar(int id, CharUpdateDto character)
        {
            var set = new ArtifactSet { Id = character.SetId };
            var wep = new Weapons { Id = character.WeaponId };


            var newChar = new Character
            {
                ArtifactSet = set,
                Weapon = wep,
                Id = id
            };
            return await _repository.UpdateChar(id, newChar);
        }

        public async Task<bool> GetNameExists(string? name)
        {
            var verify = await _repository.GetName(name);
            return verifier(verify);
        }

        public static bool verifier(int verify)
        {
            if (verify > 0)
                return true;
            return false;
        }

    }
}
