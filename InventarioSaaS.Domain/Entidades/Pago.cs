using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.Entidades
{
    public class Pago
    {
        public int Id { get; set; }
        [Required]
        public CuentasPorCobrar CuentaPorCobrar { get; set; }
        public required int CuentasPorCobrarId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int? EmpresaId { get; set; }
    }
}
