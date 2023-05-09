namespace GenshinApi.Models
{
    public class TeamByTeamId
    {
        public int Id { get; set; }
        public string TeamComp { get; set; }
        public List<Character>? Characters { get; set; } = new List<Character>();
    }
}
