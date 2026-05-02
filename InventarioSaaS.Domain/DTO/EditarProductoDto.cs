using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class EditarProductoDto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public int EmpresaId { get; set; }

        public int CategoriaId { get; set; }
    }
}
