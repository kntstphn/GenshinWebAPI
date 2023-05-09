using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;

namespace GenshinApi.Repositories.TeamCompositionRepositories
{
    public class TeamCompositionRepository : ITeamCompositionRepository
    {
        private readonly DapperContext _context;

        public TeamCompositionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTeamComposition(TeamComposition teamComp)
        {
            var query = "INSERT INTO TeamComposition (Name) VALUES (@Name); " +
                "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(query, teamComp);
            }
        }

        public async Task<bool> DeleteTeamComposition(int id)
        {
            var query = "DELETE FROM TeamComposition WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteAsync(query, new { id }) > 0) ? true : false;
            }
        }

        public async Task<IEnumerable<TeamComposition>> GetAll()
        {
            var query = "SELECT * FROM TeamComposition";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<TeamComposition>(query);
            }
        }

        public async Task<TeamComposition> GetTeamById(int id)
        {
            var query = "SELECT * FROM TeamComposition WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                var teamComposition = await con.QuerySingleOrDefaultAsync<TeamComposition>(query, new { id });

                return teamComposition;
            }
        }

        public async Task<bool> UpdateTeamComposition(int id, TeamComposition teamComp)
        {
            var query = "UPDATE TeamComposition SET Name = @Name WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteAsync(query, new { teamComp.Name,  id }) > 0) ? true : false;
            }
        }

        public async Task<bool> GetName(string name)
        {
            var sql = "SELECT COUNT(*) FROM TeamComposition WHERE [Name] = @Name";
            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteScalarAsync<int>(sql, new { Name = name }) > 0) ? true : false;
            }
        }
    }
}
