using AutoMapper;
using GenshinApi.Dtos.CharDto;
using GenshinApi.Dtos.RegionDto;
using GenshinApi.Models;
using GenshinApi.Repositories.RegionRepo;



namespace GenshinApi.Services.RegionServices
{
    public class RegionServices : IRegionServices
    {

        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;
        
        public RegionServices(IRegionRepository regionrepo, IMapper mapper)
        {
            _repository = regionrepo;
            _mapper = mapper;
        }

        public async Task<RegionDto> AddRegion(RegionCreationDto regionCreationDto)
        {
            var regionModel = new Region
            {
                Name = regionCreationDto.Name,
                RegionInspiredFrom = regionCreationDto.RegionInspiredFrom,
                RegionDescription = regionCreationDto.RegionDescription
            };

            regionModel.Id = await _repository.AddRegion(regionModel);

            return _mapper.Map<RegionDto>(regionModel);
        }

        public async Task<bool> DeleteRegionById(int id)
        {
            var regionModel = await _repository.DeleteRegion(id);
            if (regionModel == false) return false;

            return true;
        }

        public async Task<IEnumerable<CharacterDto>> GetAllCharByRegionName(string regionName)
        {
            var characterModels = await _repository.GetAllByRegionName(regionName);

            return _mapper.Map<IEnumerable<CharacterDto>>(characterModels);
        }

        public async Task<IEnumerable<RegionDto?>> GetAllRegion()
        {
            var regionModel = await _repository.GetAllRegion();
            return _mapper.Map<IEnumerable<RegionDto?>>(regionModel); 
        }

        public async Task<RegionDto?> GetRegionById(int id)
        {
            var regionModel = await _repository.GetRegionById(id);
            if (regionModel == null) return null;

            return _mapper.Map<RegionDto?>(regionModel);
        }

        public async Task<bool> UpdateRegionSetById(int id, RegionCreationDto regionCreationDto)
        {
            
            var wep = new Region
            {
                Name = regionCreationDto.Name,
                RegionInspiredFrom = regionCreationDto.RegionInspiredFrom,
                RegionDescription = regionCreationDto.RegionDescription,
                Id = id
            };
            return await _repository.UpdateRegionSetById(id, wep);
        }
    }
}
