using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerCuentasPorCobrarDto
    {
        public int Id { get; set; }
        public int VentaId { get; set; }

        public string NombreCliente { get; set; }

        public decimal MontoTotal { get; set; }

        public decimal MontoPendiente { get; set; }

        public TipoPago.Estado Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

    }
}
