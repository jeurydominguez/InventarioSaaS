
using InventarioSaaS.Web.Models.Common;

namespace InventarioSaaS.Web.Models.Sales
{
    public class CreateSaleDto
    {
        public List<ProductForSaleDto> Productos { get; set; } = [];
        public TipoPago.EstadoVenta TipoPago { get; set; }
        public int? ClienteId { get; set; }
    }
}
