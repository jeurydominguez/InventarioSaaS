using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public float PrecioVenta { get; set; }

        public int Stock { get; set; }

    }
}
