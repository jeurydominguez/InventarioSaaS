using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IRepository
{
    public interface IReportesRepository
    {
        Task<string> BuscarEmpresa();
        Task<List<Venta>> VentasPorDia(DateTime inicio, DateTime final, int empresaID);
        Task<List<ProductoTop5Dto>> ProductosMasVendidos(DateTime inicio, DateTime final, int empresaId);
        Task<List<Cliente>> ClientesConDeuda(int empresaId);
        Task<EstadoCuentasDtos> EstadoDeCuentasPorCobrar(int empresaId);
    }
}
