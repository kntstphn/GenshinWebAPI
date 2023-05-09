using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;
using System.Data;

namespace GenshinApi.Repositories.Team_CharRepositories
{
    public class Team_CharacterRepository : ITeam_CharacterRepository
    {
        private readonly DapperContext _context;

        public Team_CharacterRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<TeamByCharacterId> GetAllByCharacterId(int characterId)
        {
            var sp = "[spTeamCharacter_GetAllTeamsByCharacterId]";

            using (var con = _context.CreateConnection())
            {
                var teams = await con.QueryAsync<TeamByCharacterId, TeamComposition, Character, TeamByCharacterId> (sp, (teamChar, teamComp, character) =>
                {
                    teamChar.Id = characterId;
                    teamChar.Char = character.Name;
                    teamChar.TeamCompositions.Add(teamComp);

                    return teamChar;
                }, new { @CharacterId = characterId }, commandType: CommandType.StoredProcedure);

                return teams.GroupBy(s => s.Id).Select(g =>
                {
                    var firstTeams = g.First();
                    firstTeams.TeamCompositions = g.Select(a => a.TeamCompositions.First()).ToList();

                    return firstTeams;
                }).SingleOrDefault()!;
            }
        }

        public async Task<TeamByTeamId> GetAllByTeamId(int teamId)
        {
            var query = "SELECT * FROM Team_Character tc INNER JOIN Character c on c.Id = tc.CharacterId INNER JOIN TeamComposition t on tc.TeamId = t.Id WHERE tc.TeamId = @TeamId";

            using (var con = _context.CreateConnection())
            {
                var teams = await con.QueryAsync<TeamByTeamId, Character, TeamComposition, TeamByTeamId>(query, (teamChar, character, teamComp) =>
                {
                    teamChar.Id = teamId;
                    teamChar.TeamComp = teamComp.Name;
                    teamChar.Characters.Add(character);

                    return teamChar;
                }, new { @TeamId = teamId });

                return teams.GroupBy(s => s.Id).Select(g =>
                {
                    var firstTeams = g.First();
                    firstTeams.Characters = g.Select(a => a.Characters.First()).ToList();

                    return firstTeams;
                }).SingleOrDefault()!;
            }

        }

        public async Task<Team_Character> CreateTeamCharacter(Team_Character teamChar)
        {
            var query = "INSERT INTO Team_Character (TeamId, CharacterId) VALUES (@TeamId, @CharacterId)" +
                " SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                var teams = await con.QuerySingleAsync<Team_Character>(query, 
                    new
                    {
                        @TeamId = teamChar.TeamComp.Id,
                        @CharacterId = teamChar.Char.Id
                    });
                }

            return teamChar;
        }

        public async Task<Team_Character> GetTeamChar(int teamId, int characterId)
        {
            var query = "SELECT * FROM Team_Character WHERE TeamId = @TeamId AND CharacterId = @CharacterId";

            using (var con = _context.CreateConnection())
            {
                var teamChar = await con.QuerySingleOrDefaultAsync<Team_Character>(query, new { @TeamId = teamId, @CharacterId = characterId });

                var teams = new Team_Character()
                {
                    TeamComp = await GetTeamById(teamId),
                    Char = await GetCharById(characterId)
                };

                return teams;
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

        public async Task<Character> GetCharById(int id)
        {
            var sql = "SELECT C.Id, C.Name,C.Rarity, W.Name, R.Name, S.Name, E.Name " +
                "FROM Character C " +
                "INNER JOIN Element AS E ON C.ElemId = E.Id " +
                "INNER JOIN Weapons AS W ON C.WeaponId = W.Id " +
                "INNER JOIN Region AS R ON C.RegionId = R.Id " +
                "INNER JOIN ArtifactSet AS S ON C.PreferredArtifactSet = S.Id " +
                "WHERE C.Id = @Id";

            using (var con = _context.CreateConnection())
            {
                var wait = await con.QueryAsync<Character, Weapons, Region, ArtifactSet, Element, Character>(sql, MapCharacter, new { id }, splitOn: "Name"); ;
                return wait.Single(); //Changed from SingleOrDefault to Single
            }
        }

        private static Character MapCharacter(Character character, Weapons weapons, Region region, ArtifactSet set, Element element)
        {
            character.Weapon = weapons;
            character.ArtifactSet = set;
            character.Region = region;
            character.Element = element;
            return character;
        }

        public async Task<bool> DeleteTeamChar(int teamId, int characterId)
        {
            var sql = "DELETE FROM Team_Character WHERE TeamId = @TeamId AND CharacterId = @CharacterId";

            using (var con = _context.CreateConnection())
            {
                bool flag = (await con.ExecuteAsync(sql, new { @TeamId = teamId, @CharacterId = characterId}) > 0) ? true : false;

                Console.WriteLine(flag.ToString());
                return flag;
            }
        }
    }
}
