using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using Crud.DTOs;
using Crud.Models;

namespace Crud.Controllers
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DirectorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDto>>> GetAll()
        {
            var directores = await _context.Directors
                .Select(d => new DirectorDto
                {
                    PKDirector = d.PKDirector,
                    Name = d.Name,
                    Age = d.Age,
                    Active = d.Active
                })
                .ToListAsync();

            return Ok(directores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDto>> GetById(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return NotFound(new { message = $"No existe el Director con id {id}" });

            return Ok(new DirectorDto
            {
                PKDirector = director.PKDirector,
                Name = director.Name,
                Age = director.Age,
                Active = director.Active
            });
        }

        [HttpPost]
        public async Task<ActionResult<DirectorDto>> Create(DirectorCreateUpdateDto dto)
        {
            var director = new Director
            {
                Name = dto.Name,
                Age = dto.Age,
                Active = dto.Active
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            var result = new DirectorDto
            {
                PKDirector = director.PKDirector,
                Name = director.Name,
                Age = director.Age,
                Active = director.Active
            };

            return CreatedAtAction(nameof(GetById), new { id = director.PKDirector }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DirectorCreateUpdateDto dto)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return NotFound(new { message = $"No existe el Director con id {id}" });

            director.Name = dto.Name;
            director.Age = dto.Age;
            director.Active = dto.Active;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return NotFound(new { message = $"No existe el Director con id {id}" });

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}