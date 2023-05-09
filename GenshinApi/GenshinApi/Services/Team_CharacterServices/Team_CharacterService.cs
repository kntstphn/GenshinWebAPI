using AutoMapper;
using GenshinApi.Dtos.Team_CharacterDtos;
using GenshinApi.Models;
using GenshinApi.Repositories.Characters;
using GenshinApi.Repositories.Team_CharRepositories;
using GenshinApi.Repositories.TeamCompositionRepositories;

namespace GenshinApi.Services.Team_CharacterServices
{
    public class Team_CharacterService : ITeam_CharacterService
    {
        private readonly ITeam_CharacterRepository _repository;
        private readonly ITeamCompositionRepository _compRepository;
        private readonly ICharacterRepository _charRepository;
        private readonly IMapper _mapper;

        public Team_CharacterService(
            ITeam_CharacterRepository repository,
            IMapper mapper,
            ITeamCompositionRepository compRepository,
            ICharacterRepository charRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _compRepository = compRepository;
            _charRepository = charRepository;

        }
        public async Task<Team_CharacterDto> CreateTeamCharacter(int characterId, int teamId)
        {
            var teamModel = new Team_Character()
            {
               Char = await _charRepository.GetCharById(characterId),
               TeamComp = await _compRepository.GetTeamById(teamId)
            };

            var teams = await _repository.CreateTeamCharacter(teamModel);

            return _mapper.Map<Team_CharacterDto>(teams);
        }

        public async Task<bool> DeleteTeamChar(int teamId, int characterId)
        {
            return await _repository.DeleteTeamChar(teamId, characterId);
        }

        public async Task<Team_CharacterCharacterIdDto> GetAllByCharacterId(int characterId)
        {
            var teams = await _repository.GetAllByCharacterId(characterId);

            return _mapper.Map<Team_CharacterCharacterIdDto>(teams);
        }

        public async Task<Team_CharacterTeamIdDto> GetAllByTeamId(int teamId)
        {
            var teams = await _repository.GetAllByTeamId(teamId);

            return _mapper.Map<Team_CharacterTeamIdDto>(teams);
        }

        public async Task<Team_CharacterDto> GetTeamChar(int teamId, int characterId)
        {
            var teams = await _repository.GetTeamChar(teamId, characterId);

            return _mapper.Map<Team_CharacterDto>(teams);
        }
    }
}
