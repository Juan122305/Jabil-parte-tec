namespace Crud.DTOs
{
    public class MovieDto
    {
        public int PKMovies { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public TimeSpan? Duration { get; set; }
        public int FKDirector { get; set; }
        public string? DirectorName { get; set; }
    }

    public class MovieCreateUpdateDto
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public TimeSpan? Duration { get; set; }
        public int FKDirector { get; set; }
    }
}