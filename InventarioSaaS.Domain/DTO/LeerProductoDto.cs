using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

    }
}
