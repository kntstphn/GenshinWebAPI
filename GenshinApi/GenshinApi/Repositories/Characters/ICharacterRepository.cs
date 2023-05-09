using GenshinApi.Models;

namespace GenshinApi.Repositories.Characters
{
    public interface ICharacterRepository
    {
        /// <summary>
        /// Creates new char
        /// </summary>
        /// <param name="character">Character model</param>
        /// <returns>The id of the newly created character</returns>
        Task<int> CreateNewChar(Character character);
        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="id">Character id to be deleted</param>
        /// <returns>A message on whether or not the char with param id
        /// was sucessfully deleted</returns>
        Task<bool> DeleteChar(int id);

        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="Id">Character id to be updated</param>
        /// <param name="character">Character to be updated</param>
        /// <returns>A message on whether or not the char with param id
        /// was sucessfully updated</returns>
        Task<bool> UpdateChar(int Id, Character character);

        /// <summary>
        /// Gets one character whose id is equal to the parameter
        /// </summary>
        /// <param name="id">Character id to be updated</param>
        /// <returns>A character with matching id</returns>

        Task<Character> GetCharById(int id);

        /// <summary>
        /// Gets all characters
        /// </summary>
        /// <returns>All characters</returns>
        Task<IEnumerable<Character>> GetAll();

        /// <summary>
        /// Gets all characters with same elements
        /// </summary>
        /// <param name="elementName">Character elementName</param>
        /// <returns>All characters with matching elementName</returns>
        Task<IEnumerable<Character>> GetAllByElementName(string elementName);

        /// <summary>
        /// Gets character's weapon
        /// </summary>
        /// <param name="id">Character weaponId</param>
        /// <returns>character weapon</returns>
        Task<Weapons> GetWeapon(int id);

        /// <summary>
        /// Gets character's region
        /// </summary>
        /// <param name="id">Character regionId</param>
        /// <returns>character region</returns>
        Task<Region> GetRegion(int id);

        /// <summary>
        /// Gets character's artifact set
        /// </summary>
        /// <param name="id">Character setId</param>
        /// <returns>haracter artifact set</returns>
        Task<ArtifactSet> GetArtifactSet(int id);

        /// <summary>
        /// Gets character's element
        /// </summary>
        /// <param name="id">Character elemId</param>
        /// <returns>character element</returns>
        Task<Element> GetElement(int id);

        /// <summary>
        /// Gets character'sname
        /// </summary>
        /// <param name="name">Character name</param>
        /// <returns>character name</returns>
        Task<int> GetName(string? name);
    }
}
