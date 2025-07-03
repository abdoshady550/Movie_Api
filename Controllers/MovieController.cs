namespace Movie_Api.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Movie_Api.Data;
    using Movie_Api.Handler;
    using Movie_Api.Mappers;
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
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var AllMovies = await _service.GetAll();
            if (AllMovies == null)
                throw new ApiException($"Has No Data Back", StatusCodes.Status204NoContent);

            var dto = MovieMapper.ReadMoviesDto(AllMovies);
            return Ok(APIRespone<IEnumerable<ReadMovieDto>>.CreateSuccess(dto));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetMovieById(int id)
        {
            var Movie = await _service.GetById(id);
            if (Movie == null)
                throw new ApiException($"Not Found", StatusCodes.Status404NotFound);
            var dto = MovieMapper.ReadMovieDto(Movie);

            return Ok(APIRespone<ReadMovieDto>.CreateSuccess(dto));
        }

        [HttpGet]
        [Route("GetByGenraId")]
        public async Task<ActionResult> GetMoviesByGenra(int genraId)
        {
            var AllMovies = await _service.GetMoviesByType(genraId);

            if (AllMovies.Count() == 0)
                throw new ApiException($"No Element Found", StatusCodes.Status404NotFound);

            var dto = MovieMapper.ReadMoviesDto(AllMovies);

            return Ok(APIRespone<IEnumerable<ReadMovieDto>>.CreateSuccess(dto));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromForm] UpdateMovieDto dto)
        {
            var Movie = await _service.GetById(id);
            if (Movie == null)
                throw new ApiException($"Not Found", StatusCodes.Status404NotFound);


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
            var Moviedto = MovieMapper.ReadMovieDto(Movie);


            return Ok(APIRespone<ReadMovieDto>.CreateSuccess(Moviedto, "Updated successfully"));
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] MovieDto dto)
        {
            if (dto == null)
                throw new ApiException($"Bad Request From The Body", StatusCodes.Status400BadRequest);


            if (!_allowedExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest(error: "only .png or .jpg Extention");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = MovieMapper.CreateMovieDto(dto, dataStream);
            await _service.Add(movie);
            return Ok(APIRespone<Movie>.CreateSuccess(movie, "Created successfully"));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMovieById(int id)
        {
            var Movie = await _service.GetById(id);
            if (Movie == null)
                throw new ApiException($"Not Found", StatusCodes.Status404NotFound);

            var dto = MovieMapper.ReadMovieDto(Movie);
            _service.Delete(Movie);
            return Ok(APIRespone<ReadMovieDto>.CreateSuccess(dto, "Deleted successfully"));
        }
    }
}
