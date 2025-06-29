namespace Movie_Api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Movie_Api.Data;
    using Movie_Api.Model.Dtos;
    using Movie_Api.Model.Eintites;

    [Route("api/[controller]")]
    [ApiController]
    public class GenraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genra>>> GetAllGenra()
        {
            var AllGenra = await _context.Genras.OrderBy(e => e.Name).ToListAsync();
            return Ok(AllGenra);
        }

        [HttpPost]
        public async Task<ActionResult> AddGenra(CreateGenraDto GenraDto)
        {
            if (GenraDto == null)
                return BadRequest();

            var item = new Genra() { Name = GenraDto.Name };
            await _context.Genras.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenraById(int id, [FromBody] UpdateGenraDto GenraDto)
        {
            if (GenraDto == null)
                return BadRequest();

            var item = await _context.Genras.FindAsync(id);
            if (item == null)
                return NotFound();

            item.Name = GenraDto.Name;
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenraById(int id)
        {

            var item = await _context.Genras.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Genras.Remove(item);
            _context.SaveChanges();

            return Ok(item);
        }
    }
}
