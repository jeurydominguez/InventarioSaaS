using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerPagoDto
    {
        public int Id { get; set; }
        public int CuentasPorCobrarId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
