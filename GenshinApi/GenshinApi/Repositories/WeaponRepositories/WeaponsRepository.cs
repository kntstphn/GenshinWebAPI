using AutoMapper;
using Dapper;
using GenshinApi.Context;
using GenshinApi.Models;
using System.Data;

namespace GenshinApi.Repositories.WeaponRepositories
{
    public class WeaponsRepository : IWeaponsRepository
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;

        public WeaponsRepository(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Weapons> CreateWeapon(Weapons weapon)
        {
            var sql = "INSERT INTO Weapons(Name,Damage,WeaponType_Id,Rarity) VALUES (@Name,@Damage,@WeaponType_Id,@Rarity);" +
                "SELECT SCOPE_IDENTITY()";
            using (var con = _context.CreateConnection())
            {
                weapon.Id = await con.ExecuteScalarAsync<int>(sql,
                    new
                    {
                        @Name = weapon.Name,
                        @Damage = weapon.Damage,
                        @WeaponType_Id = weapon.Type?.Id,
                        @Rarity = weapon.Rarity
                    });
                return weapon;
            }
        }

        public async Task<bool> DeleteWeapon(int id)
        {
            var sql = "DELETE FROM Weapons WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteAsync(sql, new { Id = id }) > 0) ? true : false;
            }
        }

        public async Task<IEnumerable<Weapons>> GetAllWeapons()
        {
            var sp = "[spWeapons_GetAllWeapons]";
            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Weapons, WeaponType, Weapons>(sp, MapWeaponWeaponType, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Weapons?> GetWeapon(int id)
        {
            var sql = "[spWeapons_GetWeaponById]";
            using (var con = _context.CreateConnection())
            {
                var wait = await con.QueryAsync<Weapons, WeaponType, Weapons>(sql, MapWeaponWeaponType,
                    new { Id = id }, commandType: CommandType.StoredProcedure, splitOn: "Id");
                return wait.SingleOrDefault();
            }
        }

        public async Task<bool> UpdateWeapon(int id, Weapons weapon)
        {
            var sql = "UPDATE Weapons SET Name = @Name, Damage = @Damage, WeaponType_Id = @WeaponType_Id, " +
                "Rarity = @Rarity WHERE Id = @Id";
            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteAsync(sql,
                    new
                    {
                        Name = weapon.Name,
                        Damage = weapon.Damage,
                        WeaponType_Id = weapon.Type?.Id,
                        Rarity = weapon.Rarity,
                        Id = weapon.Id
                    }) > 0) ? true : false;
            }
        }

        private static Weapons MapWeaponWeaponType(Weapons weapon, WeaponType type)
        {
            weapon.Type = type;
            return weapon;
        }

        public async Task<bool> GetName(string? name)
        {
            var sql = "SELECT COUNT(*) FROM Weapons WHERE [Name] = @Name";
            using (var con = _context.CreateConnection())
            {
                return (await con.ExecuteScalarAsync<int>(sql, new { Name = name }) > 0) ? true : false;
            }
        }

        public async Task<IEnumerable<Weapons>> GetAllWeaponsByWeaponType(int id)
        {
            var sql = "SELECT * FROM Weapons s INNER JOIN WeaponType w on w.Id = s.WeaponType_Id WHERE WeaponType_Id = @Id";
            using(var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Weapons, WeaponType, Weapons>(sql, MapWeaponWeaponType, new { Id = id });
            }
        }

        public async Task<Weapons> GetWeaponByName(string? name)
        {
            var sql = "SELECT * FROM Weapons w INNER JOIN WeaponType t ON w.WeaponType_Id = t.Id WHERE w.Name = @Name";
            using(var con = _context.CreateConnection())
            {
                var wait = await con.QueryAsync<Weapons, WeaponType, Weapons>(sql, MapWeaponWeaponType, new {Name = name});
                return wait.Single();
            }
        }
    }
}
