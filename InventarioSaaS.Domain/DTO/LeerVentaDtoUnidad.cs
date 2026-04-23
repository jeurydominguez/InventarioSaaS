using InventarioSaaS.Domain.Entidades;
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
        public TipoPago TipoPago { get; set; }
        public int? clienteId { get; set; }
        public LeerClienteDtoVenta Cliente { get; set; }
        public Guid UsuarioId { get; set; }
        public List<LeerDetalleVentaDto> detalle { get; set; }
    }
}
