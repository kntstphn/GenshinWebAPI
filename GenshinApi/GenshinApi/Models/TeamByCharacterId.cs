namespace GenshinApi.Models
{
    public class TeamByCharacterId
    {
        public int Id { get; set; }
        public string Char { get; set; }
        public List<TeamComposition> TeamCompositions { get; set; } = new List<TeamComposition>();
    }
}
