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
        Task<List<VentasPorDiaDto>> VentaPorRango(RangoDeVentasDto dto);
        Task<List<ProductoTop5Dto>> ProductoMasVendido(DateTime inicio, DateTime final);
        Task<List<ClientesCondeudaDto>> ClientesConDeuda();
        Task<EstadoCuentasDtos> ReporteDeEstadoDeCuentas();
    }
}
