using GenshinApi.Dtos.CharDto;
using GenshinApi.Dtos.RegionDto;

namespace GenshinApi.Services.RegionServices
{
    public interface IRegionServices
    {
        /// <summary>
        /// Gets all the Region
        /// </summary>
        /// <returns>ALl the Region present in the database</returns>
        Task<IEnumerable<RegionDto?>> GetAllRegion();

        /// <summary>
        /// Gets a Region that the matches the Id provided
        /// </summary>
        /// <param name="id">The Id of the Region to be taken</param>
        /// <returns>The Region with matching Id</returns>
        Task<RegionDto?> GetRegionById(int id);

        /// <summary>
        /// Removes a Region that matches the Id provided
        /// </summary>
        /// <param name="regionId">The Id used as basis of which region to delete</param>
        /// <returns>A message confirming if it was successful or not</returns>
        Task<bool> DeleteRegionById(int regionId);

        /// <summary>
        /// Creates a new Region
        /// </summary>
        /// <param name="regionCreationDto">Region Model</param>
        /// <returns>The new Region Id that has been created </returns>
        Task<RegionDto> AddRegion(RegionCreationDto regionCreationDto);

        /// <summary>
        /// Updates a Region with the matching Id that is provided
        /// </summary>
        /// <param name="id">The Id of the Region to be updated</param>
        /// <param name="regionCreationDto"> The Region to be Updated</param>
        /// <returns>A message confirming if it was successful or not</returns>
        Task<bool> UpdateRegionSetById(int id, RegionCreationDto regionCreationDto);

        /// <summary>
        /// Gets all by Region Name
        /// </summary>
        /// <param name="regionName">Used to get the characters</param>
        /// <returns></returns>
        Task<IEnumerable<CharacterDto>> GetAllCharByRegionName(string regionName);

    }
}
