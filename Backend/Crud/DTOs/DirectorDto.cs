namespace Crud.DTOs
{
    public class DirectorDto
    {
        public int PKDirector { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public bool? Active { get; set; }
    }

    public class DirectorCreateUpdateDto
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public bool? Active { get; set; }
    }
}