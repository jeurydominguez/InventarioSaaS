using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IRepository
{
    public interface CuentasPorCobrarIRepository
    {
        Task<List<CuentasPorCobrar>> Get(int empresaId);
        Task<CuentasPorCobrar> ObtenerUno(int empresaId, int id);
        Task<string> ObtenerEmpresaId();
    }
}
