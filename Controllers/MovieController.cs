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
        [Route("{id}")]
        public async Task<ActionResult> GetMovieById(int id)
        {
            var Movie = await _context.Movies.FindAsync(id);
            if (Movie == null)
                return NotFound();


            return Ok(Movie);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var AllMovies = await _context.Movies
                .AsNoTracking()
                .OrderByDescending(m => m.Rate)
                .Include(e => e.Genra)
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    m.Rate,
                    m.Storyline,
                    m.Year,
                    m.Poster,
                    m.GenraId,
                    m.Genra.Name


                })
                .ToListAsync();
            return Ok(AllMovies);
        }
        [HttpGet]
        [Route("GetByGenraId")]
        public async Task<ActionResult> GetMoviesByGenra(int genraId)
        {
            var AllMovies = await _context.Movies
                .Where(m => m.GenraId == genraId)
                .Include(e => e.Genra)
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    m.Rate,
                    m.Storyline,
                    m.Year,
                    m.Poster,
                    m.GenraId,
                    m.Genra.Name


                })
                .ToListAsync();

            if (AllMovies.Count == 0)
                return NotFound("Not Founded");

            return Ok(AllMovies);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromForm] UpdateMovieDto dto)
        {
            var Movie = await _context.Movies.FindAsync(id);
            if (Movie == null)
                return NotFound();

            if (dto.Poster != null)
            {
                if (!_allowedExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest(error: "only .png or .jpg Extention");
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                Movie.Poster = dataStream.ToArray();
            }
            if (!string.IsNullOrWhiteSpace(dto.Title))
                Movie.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.Storyline))
                Movie.Storyline = dto.Storyline;

            if (dto.Rate.HasValue)
                Movie.Rate = dto.Rate.Value;

            if (dto.GenraId.HasValue)
                Movie.GenraId = dto.GenraId.Value;

            if (dto.Year.HasValue)
                Movie.Year = dto.Year.Value;

            await _context.SaveChangesAsync();

            return Ok(Movie);
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
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMovieById(int id)
        {
            var Movie = await _context.Movies.FindAsync(id);
            if (Movie == null)
                return NotFound();
            _context.Remove(Movie);
            await _context.SaveChangesAsync();
            return Ok("Movie Deleted ");
        }
    }
}
