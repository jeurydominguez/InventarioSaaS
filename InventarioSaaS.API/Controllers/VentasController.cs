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
            if(dto.TipoPago == TipoPago.EstadoVenta.credito && dto.ClienteId == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe seleccionar un cliente para ventas a crédito."
                });
            }
            await service.CrearVenta(dto);
            return Ok();
        }
        [HttpGet("all")]
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResponse<LeerVentasDto>>>Obtener([FromQuery]VentasQuery query)
        {
            var ventas = await service.ObtenerP(query);
            return Ok(ventas);
        }
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> DescargarFactura(int id)
        {
            var venta = await service.Obtener(id);

            if (venta is null)
                return NotFound();

            var pdf = FacturaPdfGenerator.Generar(venta);

            return File(
                pdf,
                "application/pdf",
                $"Factura-{venta.Id}.pdf");
        }

    }
}
