using AutoMapper;
using GenshinApi.Dtos.ArtifactSetDtos;
using GenshinApi.Models;
using GenshinApi.Repositories.ArtifactSetRepositories;

namespace GenshinApi.Services.ArtifactSetServices
{
    public class ArtifactSetService : IArtifactSetService
    {
        private readonly IArtifactSetRepository _repository;
        private readonly IMapper _mapper;

        public ArtifactSetService(
            IArtifactSetRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArtifactSetDto> CreateArtifactSet(ArtifactSetCreationDto artifactSetToCreate)
        {
            var artifactSetModel = new ArtifactSet
            {
                Name = artifactSetToCreate.Name,
                Description = artifactSetToCreate.Description
            };

            artifactSetModel.Id = await _repository.CreateArtifactSet(artifactSetModel);

            return _mapper.Map<ArtifactSetDto>(artifactSetModel);
        }

        public async Task<bool> DeleteArtifactSet(int id)
        {
            return await _repository.DeleteArtifactSet(id);
        }

        public async Task<IEnumerable<ArtifactSetDto?>> GetAllArtifactSets()
        {
            var artifactSets = await _repository.GetAllArtifactSets();

            return _mapper.Map<IEnumerable<ArtifactSetDto>>(artifactSets);
        }

        public async Task<ArtifactSetDto> GetArtifactSet(string name)
        {
            var artifactSet = await _repository.GetArtifactSet(name);

            return _mapper.Map<ArtifactSetDto>(artifactSet);
        }

        public async Task<ArtifactSetDto> GetArtifactSet(int id)
        {
            var artifactSet = await _repository.GetArtifactSet(id);

            return _mapper.Map<ArtifactSetDto>(artifactSet);
        }

        public async Task<bool> UpdateArtifactSet(int id, ArtifactSetUpdateDto artifactSet)
        {
            var artifactSetModel = new ArtifactSet()
            {
                Name = artifactSet.Name,
                Description = artifactSet.Description
            };

            return await _repository.UpdateArtifactSet(id, artifactSetModel);

        }
    }

}
