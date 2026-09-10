namespace Crud.Models
{
    public class Movie
    {
        public int PKMovies { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public TimeSpan? Duration { get; set; }

        public int FKDirector { get; set; }
        public Director? Director { get; set; }
    }
}