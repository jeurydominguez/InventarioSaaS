using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IService
{
    public interface IReporteService
    {
        Task<List<VentasPorDiaDto>> VentasPorDia();
        Task<List<VentasPorDiaDto>> VentaPorRango(DateTime inicio, DateTime final);
        Task<List<ProductoTop5Dto>> ProductoMasVendido(DateTime inicio, DateTime final);
        Task<List<ClientesCondeudaDto>> ClientesConDeuda();
        Task<EstadoCuentasDtos> ReporteDeEstadoDeCuentas();
        Task<GananciaNetaDto> ObtenerGanaciaNeta(DateTime inicio, DateTime final);
        Task<ReporteResumenDto> ObtenerResumen(DateTime inicio, DateTime final);
        Task<List<VentaChartDto>> ObtenerVentaRango(DateTime inicio, DateTime final);
        Task<List<NotificacionDto>> ObtenerNotificaciones();
    }
}
