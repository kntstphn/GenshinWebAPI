namespace GenshinApi.Models
{
    public class WeaponTypeWithList
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public List<WeaponWithNoType> Weapons { get; set; } = new List<WeaponWithNoType>();
    }
}
