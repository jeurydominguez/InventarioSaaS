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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var cuentas = await service.Get();
            return Ok(cuentas);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var cuenta = await service.Obtener(Id);
            return Ok(cuenta);
        }
    }
}
