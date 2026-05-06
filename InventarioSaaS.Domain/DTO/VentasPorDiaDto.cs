using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class VentasPorDiaDto
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public List<LeerDetalleVentaDto> Detalles { get; set; } = [];

        public required TipoPago.EstadoVenta TipoPago { get; set; }

        public LeerClienteDtoVenta? Cliente { get; set; }

        public LeerCuentasPorCobrarReportes? CuentaPorCobrar { get; set; }//este es de reportes

        public Guid UsuarioId { get; set; }
    }
}
