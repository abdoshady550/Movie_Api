using Microsoft.AspNetCore.Mvc;
using Movie_Api.Model.Dtos;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Services
{
    public interface IGenraService
    {
        public Task<IEnumerable<Genra>> GetAll();
        public Task<Genra> GetById(int id);
        public Task<Genra> Add(Genra genra);
        public Genra Update(Genra genra);
        public Genra Delete(Genra genra);
    }
}
