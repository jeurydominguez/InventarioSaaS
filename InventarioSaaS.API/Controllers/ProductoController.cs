using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService service;

        public ProductoController(IProductoService service)
        {
            this.service = service;
        }

        [HttpPost("crear")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Crear(CrearProductoDto dto)
        {
            await service.Crear(dto);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await service.BuscarTodos();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await service.BuscarProductoPorId(id);
            return Ok(producto);
        }

        [HttpPatch("{id:int}")]//este es patch, se hacen unos movimientos raros que tengo que explicar 
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Actualizar(int id, JsonPatchDocument<EditarProductoDto> patchDocument)
        {
            if(patchDocument == null)
            {
                return BadRequest("Vacio?");
            }
            var productoDto = await service.Editar(id);//aqui espero un dto con los datos del modelo 
            patchDocument.ApplyTo(productoDto, ModelState); //se aplica el parche (path)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.Actualizar(productoDto); //aqui se envia de nuevo al service para que se aplique la logica 
            return CreatedAtAction(nameof(ObtenerPorId), new { id = id }, patchDocument);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await service.Eliminar(id);
            return NoContent();
        }
    }
}
