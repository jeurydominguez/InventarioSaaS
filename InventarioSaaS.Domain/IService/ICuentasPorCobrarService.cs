using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IService
{
    public interface ICuentasPorCobrarService
    {
        Task<List<LeerCuentasPorCobrarDto>> Get();
        Task<LeerCuentaPorCobrarUnidadDto> Obtener(int id);
        Task<PagedResponse<LeerCuentasPorCobrarReportes>> ObtenerP(CuentrasPorCobrarQuery query);
    }
}
