using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class InventarioStatsDto
    {
        public int TotalProductos { get; set; }

        public int StockBajo { get; set; }

        public int TotalCategorias { get; set; }

        public decimal ValorInventario { get; set; }
    }
}
