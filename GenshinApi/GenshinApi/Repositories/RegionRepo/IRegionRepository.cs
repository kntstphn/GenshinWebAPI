using GenshinApi.Models;

namespace GenshinApi.Repositories.RegionRepo
{
    public interface IRegionRepository
    {
        /// <summary>
        /// Creates a new Region
        /// </summary>
        /// <param name="region">Region Model</param>
        /// <returns>The new Region Id that has been created </returns>
        Task<int> AddRegion(Region region);

        /// <summary>
        /// Removes a Region that matches the Id provided
        /// </summary>
        /// <param name="id">The Id of the Region to be removed</param>
        /// <returns>A message confirming if it was successful or not</returns>
        Task<bool> DeleteRegion(int id);

        /// <summary>
        /// Gets a Region that the matches the Id provided
        /// </summary>
        /// <param name="id">The Id of the Region to be taken</param>
        /// <returns>The Region with matching Id</returns>
        Task<Region?> GetRegionById(int id);

        /// <summary>
        /// Gets all the Region
        /// </summary>
        /// <returns>ALl the Region present in the database</returns>
        Task<IEnumerable<Region>> GetAllRegion();

        /// <summary>
        /// Updates a Region with the matching Id that is provided
        /// </summary>
        /// <param name="id">The Id of the Region to be Updated</param>
        /// <param name="region">The Region to be Updated</param>
        /// <returns>A message confirming if it was successful or not</returns>
        Task<bool> UpdateRegionSetById(int id, Region region);

        /// <summary>
        /// Gets all based on region name
        /// </summary>
        /// <param name="regionName">String used to get all characters</param>
        /// <returns></returns>
        Task<IEnumerable<Character>> GetAllByRegionName(string regionName);

    }

}
