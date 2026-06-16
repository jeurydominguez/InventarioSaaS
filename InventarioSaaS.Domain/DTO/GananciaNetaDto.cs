using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class GananciaNetaDto
    {
        public decimal Ingresos { get; set; }

        public decimal Gastos { get; set; }

        public decimal GananciaNeta { get; set; }
    }
}
