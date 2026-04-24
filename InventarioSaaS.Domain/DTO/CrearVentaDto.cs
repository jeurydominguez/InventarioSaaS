using InventarioSaaS.Domain.Entidades;
using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class CrearVentaDto
    {
        [Required]
        public required List<ProductoParaVentaDto> Productos { get; set; } = [];

        [Required]
        public required TipoPago.EstadoVenta TipoPago { get; set; }

        public int? ClienteId { get; set; }
    }
}
