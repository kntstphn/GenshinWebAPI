namespace GenshinApi.Dtos.CharDto
{
    public class CharacterDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Rarity { get; set; }
        public string? Weapon { get; set; }
        public string? Region { get; set; }
        public string? Set { get; set; }
        public string? Element { get; set; }
    }
}
