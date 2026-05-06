using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class EstadoCuentasDtos
    {
        public decimal Total { get; set; }

        public decimal Pagado { get; set; }

        public decimal Pendiente { get; set; }
    }
}
