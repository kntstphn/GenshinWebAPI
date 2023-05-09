using AutoMapper;
using GenshinApi.Dtos.TeamCompositionDtos;
using GenshinApi.Models;
using GenshinApi.Repositories.TeamCompositionRepositories;

namespace GenshinApi.Services.TeamCompositionServices
{
    public class TeamCompositionService : ITeamCompositionService
    {

        private readonly ITeamCompositionRepository _repository;
        private readonly IMapper _mapper;

        public TeamCompositionService(
            ITeamCompositionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TeamCompositionDto> CreateTeamComposition(TeamCompositionCreationDto teamComp)
        {
            var teamCompositionModel = new TeamComposition
            {
                Name = teamComp.Name
            };

            teamCompositionModel.Id = await _repository.CreateTeamComposition(teamCompositionModel);

            return _mapper.Map<TeamCompositionDto>(teamCompositionModel);
        }

        public async Task<bool> DeleteTeamComposition(int id)
        {
            return await _repository.DeleteTeamComposition(id);
        }

        public async Task<IEnumerable<TeamCompositionDto>> GetAll()
        {
            var teamComp = await _repository.GetAll();

            return _mapper.Map<IEnumerable<TeamCompositionDto>>(teamComp);
        }

        public async Task<bool> GetName(string name)
        {
            return await _repository.GetName(name);
        }

        public async Task<TeamCompositionDto> GetTeamById(int id)
        {
            var teamComp = await _repository.GetTeamById(id);

            return _mapper.Map<TeamCompositionDto>(teamComp);
        }

        public async Task<bool> UpdateTeamComposition(int id, TeamCompositionCreationDto teamComp)
        {
            var teamCompModel = new TeamComposition
            {
                Name = teamComp.Name
            };

            return await _repository.UpdateTeamComposition(id, teamCompModel);
        }

        
    }
}
