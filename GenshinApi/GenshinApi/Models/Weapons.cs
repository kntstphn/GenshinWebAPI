namespace GenshinApi.Models
{
    public class Weapons
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Damage { get; set; }
        public WeaponType? Type { get; set; }
        public int Rarity { get; set; }
    }
}
