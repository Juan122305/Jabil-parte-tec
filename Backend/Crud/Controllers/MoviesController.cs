using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using Crud.DTOs;
using Crud.Models;

namespace Crud.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
        {
            var movies = await _context.Movies
                .Include(m => m.Director)
                .Select(m => new MovieDto
                {
                    PKMovies = m.PKMovies,
                    Name = m.Name,
                    Gender = m.Gender,
                    Duration = m.Duration,
                    FKDirector = m.FKDirector,
                    DirectorName = m.Director != null ? m.Director.Name : null
                })
                .ToListAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetById(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.PKMovies == id);

            if (movie == null)
                return NotFound(new { message = $"No existe la pelicula con id {id}" });

            return Ok(new MovieDto
            {
                PKMovies = movie.PKMovies,
                Name = movie.Name,
                Gender = movie.Gender,
                Duration = movie.Duration,
                FKDirector = movie.FKDirector,
                DirectorName = movie.Director?.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> Create(MovieCreateUpdateDto dto)
        {
            var directorExiste = await _context.Directors.AnyAsync(d => d.PKDirector == dto.FKDirector);
            if (!directorExiste)
                return BadRequest(new { message = $"No existe el Director con id {dto.FKDirector}" });

            var movie = new Movie
            {
                Name = dto.Name,
                Gender = dto.Gender,
                Duration = dto.Duration,
                FKDirector = dto.FKDirector
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var result = new MovieDto
            {
                PKMovies = movie.PKMovies,
                Name = movie.Name,
                Gender = movie.Gender,
                Duration = movie.Duration,
                FKDirector = movie.FKDirector
            };

            return CreatedAtAction(nameof(GetById), new { id = movie.PKMovies }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MovieCreateUpdateDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound(new { message = $"No existe la pelicula con id {id}" });

            var directorExiste = await _context.Directors.AnyAsync(d => d.PKDirector == dto.FKDirector);
            if (!directorExiste)
                return BadRequest(new { message = $"No existe el Director con id {dto.FKDirector}" });

            movie.Name = dto.Name;
            movie.Gender = dto.Gender;
            movie.Duration = dto.Duration;
            movie.FKDirector = dto.FKDirector;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound(new { message = $"No existe la pelicula con id {id}" });

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}