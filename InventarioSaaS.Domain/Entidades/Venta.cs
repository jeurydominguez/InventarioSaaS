using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class Venta
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        public required decimal Total { get; set; }

        public List<DetalleVenta> Detalles { get; set; } = new();

        [Required]
        public required string TipoPago { get; set; } 

        public int? ClienteId { get; set; }

        public Cliente? cliente { get; set; }

        public Guid UsuarioId { get; set; }

        public int EmpresaId { get; set; }
    }
}
