using GenshinApi.Dtos.TeamCompositionDtos;

namespace GenshinApi.Services.TeamCompositionServices
{
    public interface ITeamCompositionService
    {
        /// <summary>
        /// Get all rows inside the TeamComposition Table
        /// </summary>
        /// <returns>An IEnumarable of all rows in TeamComposition Table</returns>
        Task<IEnumerable<TeamCompositionDto>> GetAll();

        /// <summary>
        /// Get a TeamComposition with the specific <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the TeamComposition</param>
        /// <returns>A TeamComposition object containing the data</returns>
        Task<TeamCompositionDto> GetTeamById(int id);

        /// <summary>
        /// Create a new <paramref name="teamComp"/> and store it in TeamComposition Table
        /// </summary>
        /// <param name="teamComp">An object containing the new TeamComposition to be Created</param>
        /// <returns>Returns the newly created Team Composition</returns>
        Task<TeamCompositionDto> CreateTeamComposition(TeamCompositionCreationDto teamComp);

        /// <summary>
        /// Updates an existing Team Composition containing the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the existing Team Composition to be updated</param>
        /// <param name="teamComp">New data to replace the existing Team Composition</param>
        /// <returns>Returns true if successful, else false</returns>
        Task<bool> UpdateTeamComposition(int id, TeamCompositionCreationDto teamComp);

        /// <summary>
        /// Deletes an existing Team Composition containing the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the existing Team Composition to be Deleted</param>
        /// <returns>Returns true if successful, else false</returns>
        Task<bool> DeleteTeamComposition(int id);

        /// <summary>
        /// Get Team Composition that matches <paramref name="name"/>
        /// </summary>
        /// <param name="name">Name of the existing Team Composition</param>
        /// <returns>Returns true if exists, else false</returns>
        Task<bool> GetName(string name);
    }
}
