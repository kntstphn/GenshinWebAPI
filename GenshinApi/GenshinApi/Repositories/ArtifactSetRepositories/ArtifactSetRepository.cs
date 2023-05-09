using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;

namespace GenshinApi.Repositories.ArtifactSetRepositories
{
    public class ArtifactSetRepository : IArtifactSetRepository
    {
        private readonly DapperContext _context;

        public ArtifactSetRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateArtifactSet(ArtifactSet artifactSet)
        {
            var query = "INSERT INTO ArtifactSet (Name, Description) VALUES (@Name, @Description); " +
                "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(query, artifactSet);
            }
        }

        public async Task<bool> DeleteArtifactSet(int id)
        {
            var query = "DELETE FROM ArtifactSet WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteAsync(query, new { id }) > 0) ? true : false;
            }
        }

        public async Task<IEnumerable<ArtifactSet>> GetAllArtifactSets()
        {
            var query = "SELECT * FROM ArtifactSet";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<ArtifactSet>(query);
            }
        }

        public async Task<ArtifactSet> GetArtifactSet(string name)
        {
            var query = "SELECT * FROM ArtifactSet WHERE Name = @Name";

            using (var con = _context.CreateConnection())
            {
                var artifactSet = await con.QuerySingleOrDefaultAsync<ArtifactSet>(query, new { name });

                return artifactSet;
            }
        }

        public async Task<ArtifactSet> GetArtifactSet(int id)
        {
            var query = "SELECT * FROM ArtifactSet WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                var artifactSet = await con.QuerySingleOrDefaultAsync<ArtifactSet>(query, new { id });

                return artifactSet;
            }
        }

        public async Task<bool> UpdateArtifactSet(int id, ArtifactSet artifactSet)
        {
            var query = "UPDATE  ArtifactSet SET Name = @Name, Description = @Description WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteAsync(query, new { artifactSet.Name, artifactSet.Description, id })) > 0 ? true : false;
            }
        }
    }
}
