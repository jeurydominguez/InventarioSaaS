using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/pago")]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService service;
        public PagoController(IPagoService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Crear(CrearPagoDto dto)
        {
            await service.CrearPago(dto);
            return Created();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await service.GetAll();
            return Ok(dtos);
        }

        [HttpGet("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id)
        {
            var dto = await service.Get(Id);
            return Ok(dto);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResponse<LeerPagoDto>>> Obtener([FromQuery] PagoQuery query)
        {
            var pagos = await service.Obtener(query);
            return Ok(pagos);
        }
    }
}
