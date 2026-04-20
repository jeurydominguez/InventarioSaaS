using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class ProductoParaVentaDto
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        public required int Cantidad { get; set; }

        public decimal Total { get; set; }
    }
}
