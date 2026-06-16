using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace InventarioSaaS.API.Controllers
{
    [ApiController]
    [Route("api/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService service;
        public ReportesController(IReporteService service)
        {
            this.service = service;
        }

        [HttpGet("resumen")]
        [Authorize]
        public async Task<ActionResult<ReporteResumenDto>> ObtenerResumen([FromQuery]DateTime Inicio, [FromQuery]DateTime Final)
        {
            var resumen = await service.ObtenerResumen(Inicio, Final);
            return Ok(resumen);
        }
        

        [HttpGet("ventas-hoy")]
        [Authorize]
        public async Task<IActionResult> VentasHoy()
        {
            var dtos = await service.VentasPorDia();
            return Ok(dtos);
        }

        [HttpGet("ventas-por-fecha")]
        [Authorize]
        public async Task<IActionResult> VentasPorFecha([FromQuery] DateTime inicio, [FromQuery] DateTime final)
        {
            var dtos = await service.VentaPorRango(inicio, final);
            return Ok(dtos);
        }

        [HttpGet("ganancia-neta")]
        [Authorize]
        public async Task<IActionResult> GananciaNeta([FromQuery] DateTime Inicio, [FromQuery] DateTime Final)
        {
            var resultado = await service.ObtenerGanaciaNeta(Inicio, Final);
            return Ok(resultado);
        }

        [HttpGet("productos-mas-vendidos")]
        [Authorize]
        public async Task<IActionResult> ProductosMasVendidos([FromQuery]DateTime inicio, [FromQuery]DateTime final)
        {
            var top = await service.ProductoMasVendido(inicio, final);
            return Ok(top);
        }

        [HttpGet("clientes-con-deuda")]
        [Authorize]
        public async Task<IActionResult> ClientesConDeuda()
        {
            var dtos = await service.ClientesConDeuda();
            return Ok(dtos);
        }

        [HttpGet("resumen-financiero")]
        [Authorize]
        public async Task<IActionResult> Resumenfinanciero()
        {
            var resumen = await service.ReporteDeEstadoDeCuentas();
            return Ok(resumen);
        }
        [HttpGet("ventas-para-chart")]
        [Authorize]
        public async Task<ActionResult<List<VentaChartDto>>> VentasParaChart([FromQuery]DateTime inicio, [FromQuery]DateTime final)
        {
            var dato = await service.ObtenerVentaRango(inicio, final);
            return Ok(dato);
        }
        [HttpGet("notificacion")]
        [Authorize]
        public async Task<ActionResult<List<NotificacionDto>>> Obtener()
        {
            return Ok(await service.ObtenerNotificaciones());
        }
    }
}
