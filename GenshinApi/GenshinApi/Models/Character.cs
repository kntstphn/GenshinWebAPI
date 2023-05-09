using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinApi.Models
{
    
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Rarity { get; set; }
        public string? Gender { get; set; }
        public Weapons? Weapon {get; set; }
        public Region? Region { get; set; }
        public ArtifactSet? ArtifactSet { get; set; }
        public Element? Element { get; set; }

    }
}
