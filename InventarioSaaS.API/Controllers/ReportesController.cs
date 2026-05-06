using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("ventas-hoy")]
        [Authorize]
        public async Task<IActionResult> VentasHoy()
        {
            var dtos = await service.VentasPorDia();
            return Ok(dtos);
        }

        [HttpGet("ventas-por-fecha")]
        [Authorize]
        public async Task<IActionResult> VentasPorFecha(RangoDeVentasDto dto)
        {
            var dtos = await service.VentaPorRango(dto);
            return Ok(dtos);
        }

        [HttpGet("productos-mas-vendidos")]
        [Authorize]
        public async Task<IActionResult> ProductosMasVendidos(DateTime inicio, DateTime final)
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
    }
}
