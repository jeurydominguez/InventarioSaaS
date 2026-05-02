using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService service;
        public CategoriaController(ICategoriaService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(policy:"admin")]
        public async Task<IActionResult>Post(CategoriaDto dto)
        {
            await service.Crear(dto);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await service.Get();
            return Ok(dtos);
        }

        [HttpGet("{Id:int}")]
        [Authorize]
        public async Task<IActionResult>Get(int Id)
        {
            var dto = await service.ObtenerPorId(Id);
            return Ok(dto);
        }

        [HttpDelete("{Id:int}")]
        [Authorize(policy:"admin")]
        public async Task<IActionResult>Delete(int id)
        {
            await service.Eliminar(id);
            return NoContent();
        }
    }
}
