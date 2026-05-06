using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerCuentasPorCobrarReportes
    {
        public int Id { get; set; }

        public int VentaID { get; set; }

        public decimal MontoTotal { get; set; }

        public decimal MontoPendiente { get; set; }

        public TipoPago.Estado Estado { get; set; }
    }
}
