using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class CrearProductoDto
    {
        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required float PrecioVenta { get; set; }

        [Required]
        public required int Stock { get; set; }

        public int EmpresaId { get; set; }

        public int CategoriaId { get; set; }
    }
}
