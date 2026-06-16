using InventarioSaaS.Web.Models.Customers;
using static InventarioSaaS.Web.Models.Common.TipoPago;

namespace InventarioSaaS.Web.Models.Sales
{
    public class LeerVentaDtoUnidad
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public EstadoVenta TipoPago { get; set; }

        public int? ClienteId { get; set; }

        public CustomerDto? Cliente { get; set; }

        public string UsuarioId { get; set; }

        public List<LeerDetalleVentaDto> Detalle { get; set; } = [];
    }
}
