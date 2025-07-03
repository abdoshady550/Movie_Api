using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Api.Data;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.OrderBy(e => e.Id).Include(g => g.Genra).ToListAsync();

        }
        public async Task<Movie> GetById(int id)
        {
            var item = await _context.Movies.FindAsync(id);
            if (item == null)
                return null;

            return item;
        }
        public async Task<Movie> Add(Movie Movie)
        {

            await _context.Movies.AddAsync(Movie);
            await _context.SaveChangesAsync();
            return Movie;

        }


        public Movie Delete(Movie Movie)
        {

            _context.Movies.Remove(Movie);
            _context.SaveChanges();
            return Movie;
        }



        public Movie Update(Movie Movie)
        {

            _context.Update(Movie);
            _context.SaveChanges();
            return Movie;

        }

        public Task<List<Movie>> GetMoviesByType(int genraId)
        {
            var AllMovies = _context.Movies
                .Where(m => m.GenraId == genraId)
              .ToListAsync();
            return AllMovies;

        }
    }
}
