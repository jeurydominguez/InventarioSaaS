using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class ReporteResumenDto
    {
        public decimal Ingresos { get; set; }

        public int Facturas { get; set; }

        public int Clientes { get; set; }

        public decimal Gastos { get; set; }

        public decimal Conversion { get; set; }
    }
}
