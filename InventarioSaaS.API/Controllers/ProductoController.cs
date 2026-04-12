using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("obtener-todos")]
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

        [HttpPut("{id:int}")]//cambiar a patch, ver documentacion para implementar
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Actualizar(int id, EditarProductoDto dto)
        {
            await service.Editar(id, dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = id }, dto);
        }
    }
}
