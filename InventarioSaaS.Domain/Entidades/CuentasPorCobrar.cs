using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.Entidades
{
    public class CuentasPorCobrar
    {
        public int Id { get; set; }

        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public decimal MontoTotal { get; set; }

        public decimal MontoPendiente { get; set; }

        public TipoPago.Estado Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int EmpresaId { get; set; }
    }
}
