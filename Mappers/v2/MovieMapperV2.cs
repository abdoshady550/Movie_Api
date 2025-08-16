using AutoMapper;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Dtos.V2;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Mappers
{
    public static class MovieMapperV2
    {
        //public static Movie CreateMovieDto(MovieDto2 dto, MemoryStream dataStream)
        //{


        //    return new Movie
        //    {

        //        Title = dto.Title,
        //        Rate = dto.Rate,
        //        Storyline = dto.Storyline,
        //        Year = dto.Year,
        //        Poster = dataStream.ToArray(),
        //        GenraId = dto.GenraId,


        //    };
        //}
        public static IEnumerable<ReadMovieDto2> ReadMoviesDto(IEnumerable<Movie> movies)
        {


            return movies.Select(m => new ReadMovieDto2
            {
                Id = m.Id,
                Title = m.Title,
                Rate = m.Rate,
                Storyline = m.Storyline,
                Year = m.Year,
                PosterUrl = m.PosterUrl,
                GenraId = m.GenraId,
                GenraName = m.Genra?.Name,
            });
        }

        public static ReadMovieDto2 ReadMovieDto(Movie movie)
        {


            return new ReadMovieDto2
            {
                Id = movie.Id,
                Title = movie.Title,
                Rate = movie.Rate,
                Storyline = movie.Storyline,
                Year = movie.Year,
                PosterUrl = movie.PosterUrl,
                GenraId = movie.GenraId,
                GenraName = movie.Genra?.Name,
            };
        }

    }
}
