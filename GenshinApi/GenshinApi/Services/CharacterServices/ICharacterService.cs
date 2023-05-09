using GenshinApi.Dtos.CharDto;

namespace GenshinApi.Services.CharacterServices
{
    public interface ICharacterService
    { /// <summary>
      /// Creates new char
      /// </summary>
      /// <param name="charToCreate">Character Creation Dto</param>
      /// <returns>The newly created character</returns>
        Task<CharacterDto> CreateChar(CharacterCreationDto charToCreate);

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
        /// <param name="id">Character id to be updated</param>
        /// <param name="character">Character to be updated</param>
        /// <returns>A message on whether or not the char with param id
        /// was sucessfully updated</returns>
        Task<bool> UpdateChar(int id, CharUpdateDto character);

        /// <summary>
        /// Gets one character whose id is equal to the parameter
        /// </summary>
        /// <param name="id">Character id to be updated</param>
        /// <returns>A character dto with matching id</returns>
        Task<CharacterDto> GetCharById(int id);

        /// <summary>
        /// Gets all characters
        /// </summary>
        /// <returns>All characters</returns>
        Task<IEnumerable<CharacterDto>> GetAllChar();

        /// <summary>
        /// Gets all characters with same elements
        /// </summary>
        /// <param name="elementName">Character elementName</param>
        /// <returns>All characters with matching elementName</returns>
        Task<IEnumerable<CharacterDto>> GetAllCharByElementName(string elementName);

        /// <summary>
        /// Confirms if character with param name exists in DB
        /// </summary>
        /// <param name="name">Character name</param>
        /// <returns>True or false</returns>
        Task<bool> GetNameExists(string? name);

    }
}
