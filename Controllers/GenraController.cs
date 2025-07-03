namespace Movie_Api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Movie_Api.Data;
    using Movie_Api.Handler;
    using Movie_Api.Model.Dtos;
    using Movie_Api.Model.Eintites;
    using Movie_Api.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class GenraController : ControllerBase
    {

        private readonly IGenraService _service;


        public GenraController(IGenraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genra>>> GetAllGenra()
        {
            var AllGenra = await _service.GetAll();
            return Ok(APIRespone<IEnumerable<Genra>>.CreateSuccess(AllGenra));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetGenraById(int id)
        {
            var item = await _service.GetById(id);
            if (item == null)
                throw new ApiException($"Not Found", StatusCodes.Status404NotFound);


            return Ok(APIRespone<Genra>.CreateSuccess(item));
        }
        [HttpPost]
        public async Task<ActionResult> AddGenra(CreateGenraDto GenraDto)
        {
            if (GenraDto == null)
                throw new ApiException($"BadRequest from body", StatusCodes.Status400BadRequest);

            var item = new Genra() { Name = GenraDto.Name };
            await _service.Add(item);
            return Ok(APIRespone<Genra>.CreateSuccess(item));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenraById(int id, [FromBody] UpdateGenraDto GenraDto)
        {
            if (GenraDto == null)
                throw new ApiException($"BadRequest from body", StatusCodes.Status400BadRequest);

            var item = await _service.GetById(id);
            if (item == null)
                throw new ApiException($"Not Found", StatusCodes.Status404NotFound);

            item.Name = GenraDto.Name;
            _service.Update(item);

            return Ok(APIRespone<Genra>.CreateSuccess(item));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenraById(int id)
        {

            var item = await _service.GetById(id);
            if (item == null)
                throw new ApiException($"Not Found", StatusCodes.Status404NotFound);

            _service.Delete(item);

            return Ok(APIRespone<Genra>.CreateSuccess(item));
        }
    }
}
