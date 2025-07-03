namespace Movie_Api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Movie_Api.Data;
    using Movie_Api.Model.Dtos;
    using Movie_Api.Model.Eintites;
    using Movie_Api.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        private List<string> _allowedExtention = new List<string> { ".jpg", ".png" };

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetMovieById(int id)
        {
            var Movie = await _service.GetById(id);
            if (Movie == null)
                return NotFound();

            return Ok(Movie);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var AllMovies = await _service.GetAll();
            return Ok(AllMovies);
        }

        [HttpGet]
        [Route("GetByGenraId")]
        public async Task<ActionResult> GetMoviesByGenra(int genraId)
        {
            var AllMovies = await _service.GetMoviesByType(genraId);

            if (AllMovies.Count() == 0)
                return NotFound("Not Founded");

            return Ok(AllMovies);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromForm] UpdateMovieDto dto)
        {
            var Movie = await _service.GetById(id);
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

            _service.Update(Movie);

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
            await _service.Add(movie);
            return Ok(movie);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMovieById(int id)
        {
            var Movie = await _service.GetById(id);
            if (Movie == null)
                return NotFound();

            _service.Delete(Movie);
            return Ok("Movie Deleted ");
        }
    }
}
