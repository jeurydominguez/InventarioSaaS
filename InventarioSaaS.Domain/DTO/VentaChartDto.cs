using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class VentaChartDto
    {
        public string Label { get; set; } = "";
        public decimal Total { get; set; }
    }
}
