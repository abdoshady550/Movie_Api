using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Api.Data;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _context;
        private List<string> _allowedExtention = new List<string> { ".jpg", ".png" };
        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genra>>> GetAllGenra()
        {
            var AllMovies = await _context.Movies.ToListAsync();
            return Ok(AllMovies);
        }
        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieDto dto)
        {
            if (dto == null)
                return BadRequest();

            if (!_allowedExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest(error: "only .png or .jpg Extention");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = new Movie()
            {
                Title = dto.Title,
                Rate = dto.Rate,
                Storyline = dto.Storyline,
                Year = dto.Year,
                Poster = dataStream.ToArray(),
                GenraId = dto.GenraId,

            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }
    }
}
