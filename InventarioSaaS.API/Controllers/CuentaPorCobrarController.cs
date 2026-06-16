using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/cuenta")]
    public class CuentaPorCobrarController : ControllerBase
    {
        private readonly ICuentasPorCobrarService service;

        public CuentaPorCobrarController(ICuentasPorCobrarService service)
        {
            this.service = service;
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var cuentas = await service.Get();
            return Ok(cuentas);
        }

        [HttpGet("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id)
        {
            var cuenta = await service.Obtener(Id);
            return Ok(cuenta);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResponse<LeerCuentasPorCobrarReportes>>> Obtener([FromQuery]CuentrasPorCobrarQuery query)
        {
            var cuentas = await service.ObtenerP(query);
            return Ok(cuentas);
        }
    }
}
