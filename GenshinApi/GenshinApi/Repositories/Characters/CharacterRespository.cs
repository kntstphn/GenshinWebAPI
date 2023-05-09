using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;
using System.Data;

namespace GenshinApi.Repositories.Characters
{
    public class CharacterRespository : ICharacterRepository
    {
        private readonly DapperContext _context;

        public CharacterRespository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> CreateNewChar(Character character)
        {
            var sql = "INSERT INTO Character (Name, Rarity, Gender, WeaponId, RegionId, PreferredArtifactSet, Elemid ) VALUES (@Name, @Rarity, @Gender, @WeaponId, @RegionId, @PreferredArtifactSet, @Elemid); " +
                 "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {

                return await con.ExecuteScalarAsync<int>(sql, new { character.Name, character.Rarity, character.Gender, WeaponId = character.Weapon?.Id,
                RegionId = character.Region?.Id, PreferredArtifactSet = character.ArtifactSet?.Id, ElemId = character.Element?.Id});
            }
        }

        public async Task<bool> DeleteChar(int id)
        {
            var sql = "DELETE FROM Character WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                var verify = await con.ExecuteAsync(sql, new { Id = id });
                return verifier(verify);
            }
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            var sql = "SELECT *" +
                " FROM Character C" +
                " INNER JOIN Weapons W ON C.WeaponId = W.Id" +
                " INNER JOIN Region R ON C.RegionId = R.Id" +
                " INNER JOIN ArtifactSet S ON C.PreferredArtifactSet = S.Id" +
                " INNER JOIN Element E ON C.ElemId = E.Id";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Character, Weapons, Region, ArtifactSet, Element, Character>(sql, MapCharacter);
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

        private static Character MapCharacterElement(Character character, Element element)
        {
            character.Element = element;
            return character;
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
                var wait = await con.QueryAsync<Character, Weapons, Region, ArtifactSet, Element, Character>(sql, MapCharacter, new { id }, splitOn: "Name"); 
                return wait.Single(); //Changed from SingleOrDefault to Single
            }
        }

        public async Task<bool> UpdateChar(int Id, Character character)
        {
            var sql = "UPDATE Character SET  PreferredArtifactSet = @PreferredArtifactSet, WeaponId = @WeaponId " +
                "WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                var verify = await con.ExecuteAsync(sql,
                    new
                    {
                       PreferredArtifactSet = character.ArtifactSet?.Id,
                       WeaponId = character.Weapon?.Id,
                       Id = character.Id
                    });
                return verifier(verify);
            }
           
        }

        public async Task<IEnumerable<Character>> GetAllByElementName(string elementName)
        {
            var sp = "[spCharacter_GetAllByElementName]";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Character, Weapons, Region, ArtifactSet, Element, Character>(
                    sp,
                    MapCharacter,
                    new { elementName },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id");
            }
        }

        public async Task<Weapons> GetWeapon(int id)
        {
            var sql = "SELECT * FROM Weapons WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<Weapons>(sql, new { Id = id });
            }
        }

        public async Task<Region> GetRegion(int id)
        {
            var sql = "SELECT * FROM Region WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<Region>(sql, new { Id = id });
            }
        }

        public async Task<ArtifactSet> GetArtifactSet(int id)
        {
            var sql = "SELECT * FROM ArtifactSet WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<ArtifactSet>(sql, new { Id = id });
            }
        }

        public async Task<Element> GetElement(int id)
        {
            var sql = "SELECT * FROM Element WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<Element>(sql, new { Id = id });
            }
        }
        public async Task<int> GetName(string? name)
        {
            var sql = "SELECT COUNT(*) FROM Character WHERE [Name] = @Name";
            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Name = name });
            }
        }
        public static bool verifier(int verify)
        {
            return (verify > 0) ? true : false;

        }
    }
}
