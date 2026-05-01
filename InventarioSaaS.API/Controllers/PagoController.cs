using InventarioSaaS.Domain.DTO;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await service.GetAll();
            return Ok(dtos);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var dto = await service.Get(Id);
            return Ok(dto);
        }
    }
}
