using AutoMapper;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Mappers
{
    public static class MovieMapper
    {
        public static Movie CreateMovieDto(MovieDto dto, MemoryStream dataStream)
        {


            return new Movie
            {

                Title = dto.Title,
                Rate = dto.Rate,
                Storyline = dto.Storyline,
                Year = dto.Year,
                Poster = dataStream.ToArray(),
                GenraId = dto.GenraId,


            };
        }
        public static IEnumerable<ReadMovieDto> ReadMoviesDto(IEnumerable<Movie> movies)
        {


            return movies.Select(m => new ReadMovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Rate = m.Rate,
                Storyline = m.Storyline,
                Year = m.Year,
                Poster = m.Poster,
                GenraId = m.GenraId,
                GenraName = m.Genra?.Name,
            });
        }

        public static ReadMovieDto ReadMovieDto(Movie movie)
        {


            return new ReadMovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Rate = movie.Rate,
                Storyline = movie.Storyline,
                Year = movie.Year,
                Poster = movie.Poster,
                GenraId = movie.GenraId,
                GenraName = movie.Genra?.Name,
            };
        }

    }
}
