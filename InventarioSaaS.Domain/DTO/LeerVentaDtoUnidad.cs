using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerVentaDtoUnidad
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string TipoPago { get; set; }
        public int? clienteId { get; set; }
        public Guid UsuarioId { get; set; }
        public List<LeerDetalleVentaDto> detalle { get; set; }
    }
}
