using GenshinApi.Dtos.Team_CharacterDtos;

namespace GenshinApi.Services.Team_CharacterServices
{
    public interface ITeam_CharacterService
    {
        /// <summary>
        /// Get All Characters under <paramref name="teamId"/>
        /// </summary>
        /// <param name="teamId">Id of the Team Composition</param>
        /// <returns>Team Composition with the names of the characters within</returns>
        Task<Team_CharacterTeamIdDto> GetAllByTeamId(int teamId);

        /// <summary>
        /// Get All Teams of the character <paramref name="characterId"/>
        /// </summary>
        /// <param name="characterId">Id of  the character</param>
        /// <returns>Character with a list of all teams it is on</returns>
        Task<Team_CharacterCharacterIdDto> GetAllByCharacterId(int characterId);

        /// <summary>
        /// Add an Existing character to an existing Team
        /// </summary>
        /// <param name="teamChar">Object Containing the details for team and character</param>
        /// <returns>An object containing the objects for Team Composition and Character</returns>
        Task<Team_CharacterDto> CreateTeamCharacter(int characterId, int teamId);

        /// <summary>
        /// Get data that contains <paramref name="teamId"/> and <paramref name="characterId"/>
        /// </summary>
        /// <param name="teamId">Id of Team Composition</param>
        /// <param name="characterId">Character Id</param>
        /// <returns>An object that stores the specific Team of the specific character</returns>
        Task<Team_CharacterDto> GetTeamChar(int teamId, int characterId);

        /// <summary>
        /// Delete an existing row of the Table
        /// </summary>
        /// <param name="teamId">Team Composition Id</param>
        /// <param name="characterId">Id of the Character</param>
        /// <returns>Returns true if successful, else false</returns>
        Task<bool> DeleteTeamChar(int teamId, int characterId);
    }
}
