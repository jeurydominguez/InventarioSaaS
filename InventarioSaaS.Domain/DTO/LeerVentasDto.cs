using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerVentasDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public TipoPago.EstadoVenta TipoPago { get; set; }
        public int? clienteId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
