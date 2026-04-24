using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService service;
        public VentasController(IVentaService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CrearVenta(CrearVentaDto dto)
        {
            if(dto.TipoPago == TipoPago.EstadoVenta.credito && dto.ClienteId is null)
            {
                return BadRequest("Es necesario el cliente para aplicar credito");
            }
            await service.CrearVenta(dto);
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerTodos()
        {
            var item = await service.ObtenerVentas();
            return Ok(item);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Get(int id)
        {
            var venta = await service.Obtener(id);
            return Ok(venta);
        }
    }
}
