using Microsoft.AspNetCore.Mvc;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetAll();
        public Task<Movie> GetById(int id);
        public Task<Movie> Add(Movie Movie);
        public Movie Update(Movie Movie);
        public Movie Delete(Movie Movie);
        Task<List<Movie>> GetMoviesByType(int genraId);
    }
}
