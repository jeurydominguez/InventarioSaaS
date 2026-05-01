using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IRepository
{
    public interface IPagoRepository
    {
        Task<int> ObtenerEmpresaId();
        Task<List<Pago>> GetAll(int? empresaId);
        Task<Pago> ObtenerPorId(int? empresaId, int id);
        Task<CuentasPorCobrar> ObtenerCuentaPorCobrar(int empresaId, int cuentaId);
        Task GuardarPago(Pago pago, CuentasPorCobrar cuenta);
    }
}
