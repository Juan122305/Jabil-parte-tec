using System.Text.Json.Serialization;

namespace Crud.Models
{
    public class Director
    {
        public int PKDirector { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public bool? Active { get; set; }

        [JsonIgnore]
        public ICollection<Movie>? Movies { get; set; }
    }
}