using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Api.Data;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Services
{
    public class GenraService : IGenraService
    {
        private readonly AppDbContext _context;

        public GenraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genra>> GetAll()
        {
            return await _context.Genras.OrderBy(e => e.Name).ToListAsync();

        }
        public async Task<Genra> GetById(int id)
        {
            var item = await _context.Genras.FindAsync(id);
            if (item == null)
                return null;

            return item;
        }
        public async Task<Genra> Add(Genra genra)
        {

            await _context.Genras.AddAsync(genra);
            await _context.SaveChangesAsync();
            return genra;

        }


        public Genra Delete(Genra genra)
        {

            _context.Genras.Remove(genra);
            _context.SaveChanges();
            return genra;
        }



        public Genra Update(Genra genra)
        {

            _context.Update(genra);
            _context.SaveChanges();
            return genra;

        }
    }
}
