using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;

namespace GenshinApi.Repositories.WeaponTypeRepositories
{
    public class WeaponTypeRepository : IWeaponTypeRepository
    {
        private readonly DapperContext _context;

        public WeaponTypeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeaponTypeWithList>> GetAllWeaponType()
        {
            var sql = "SELECT * FROM WeaponType a INNER JOIN Weapons b on a.Id = b.WeaponType_Id";
            using(var con = _context.CreateConnection())
            {
                var wepType = await con.QueryAsync<WeaponTypeWithList, WeaponWithNoType, WeaponTypeWithList>(sql, (WeaponType, Weapons) =>
                {
                    WeaponType.Weapons.Add(Weapons);
                    return WeaponType;
                });
                return wepType.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Weapons = g.SelectMany(p => p.Weapons).ToList();
                    return groupedPost;
                });
            }
        }

        public async Task<WeaponTypeId> GetWeaponById(int wepId)
        {
            var sql = "SELECT WeaponType_Id FROM Weapons WHERE Id = @Id";
            using(var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<WeaponTypeId>(sql, new { Id = @wepId });
            }
        }

        public async Task<bool> GetId(int id)
        {
            var sql = "SELECT COUNT(*) FROM WeaponType WHERE id = @Id";
            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteScalarAsync<int>(sql, new { Id = id }) > 0) ? true : false;
            }
        }

        public async Task<WeaponType> GetTypeById(int id)
        {
            var sql = "SELECT * FROM WeaponType WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleAsync<WeaponType>(sql, new { Id = @id });
            }
        }
    }
}

