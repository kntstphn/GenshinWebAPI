using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;
using System.Data;

namespace GenshinApi.Repositories.RegionRepo
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DapperContext _context;

        public RegionRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> AddRegion(Region region)
        {
            var sql = "INSERT INTO Region (Name, RegionInspiredFrom, RegionDescription) VALUES (@Name, @RegionInspiredFrom, @RegionDescription);" +
                "SELECT SCOPE_IDENTITY();";

            using var c = _context.CreateConnection();
            return await c.ExecuteScalarAsync<int>(sql, region);
        }

        public async Task<bool> DeleteRegion(int id)
        {
            var sql = "DELETE FROM Region WHERE Id = @Id";

            using (var c = _context.CreateConnection())
            {
                return (await c.ExecuteAsync(sql, new { id }) > 0) ? true : false;
            }
        }

        public async Task<IEnumerable<Region>> GetAllRegion()
        {
            var sql = "SELECT * FROM Region";

            using var c = _context.CreateConnection();
            return await c.QueryAsync<Region>(sql);
        }

        public async Task<Region?> GetRegionById(int id)
        {
            var sql = "SELECT * FROM Region WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<Region>(sql, new { Id = id });
            }
        }

        public async Task<bool> UpdateRegionSetById(int id, Region region)
        {
            var sql = "UPDATE Region SET Name = @Name, RegionInspiredFrom = @RegionInspiredFrom, RegionDescription = @RegionDescription WHERE Id = @id";
            using (var c = _context.CreateConnection())
            {
                var verify = await c.ExecuteAsync(sql,
                    new
                    {
                        Id = region.Id,
                        Name = region.Name,
                        RegionInspiredFrom = region.RegionInspiredFrom,
                        RegionDescription = region.RegionDescription,
                    });
                return verifier(verify);
            }

        }

        public static bool verifier(int verify)
        {
            return (verify > 0) ? true : false;
        }

        public async Task<IEnumerable<Character>> GetAllByRegionName(string regionName)
        {
            var sp = "[spRegion_GetAllCharByRegionName]";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Character, Weapons, Region, ArtifactSet, Element, Character>(
                    sp,
                    MapCharacter,
                    new { regionName },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id");
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
    }
}
