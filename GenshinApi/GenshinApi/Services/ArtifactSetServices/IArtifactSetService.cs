using GenshinApi.Dtos.ArtifactSetDtos;

namespace GenshinApi.Services.ArtifactSetServices
{
    public interface IArtifactSetService
    {
        /// <summary>
        /// Gets all rows of ArtifactSet Table in Genshin Database
        /// </summary>
        /// <returns>An IEnumerable containing all data/rows in ArtifactSet Table</returns>
        Task<IEnumerable<ArtifactSetDto?>> GetAllArtifactSets();

        /// <summary>
        /// Get specific row in ArtifactSet Table containing the <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns the ArtifactSet</returns>
        Task<ArtifactSetDto> GetArtifactSet(string name);

        /// <summary>
        /// Get specific row in ArtifactSet Table containing the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of an Artifact Set</param>
        /// <returns>Returns an ArtifactSet</returns>
        Task<ArtifactSetDto> GetArtifactSet(int id);

        /// <summary>
        /// Create a new <paramref name="artifactSet"/> and store it in ArtifactSet Table
        /// </summary>
        /// <param name="artifactSet">Object containing the Artifact Set Data</param>
        /// <returns>Returns the newly created ArtifactSet</returns>
        Task<ArtifactSetDto> CreateArtifactSet(ArtifactSetCreationDto artifactSet);

        /// <summary>
        /// Updates an existing ArtifactSet with <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the Artifact Set to be updated</param>
        /// <param name="artifactSet">The new data to replace the existing data of an Artifact Set</param>
        /// <returns>Returns true if successful, else false</returns>
        Task<bool> UpdateArtifactSet(int id, ArtifactSetUpdateDto artifactSet);

        /// <summary>
        /// Deletes an existing Artifact Set with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the Artifact Set to be deleted</param>
        /// <returns>Returns true if successful, else return false</returns>
        Task<bool> DeleteArtifactSet(int id);
    }
}
