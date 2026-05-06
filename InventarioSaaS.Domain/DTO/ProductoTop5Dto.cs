using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class ProductoTop5Dto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadVendida { get; set; }
    }
}
