using InventarioSaaS.Web.Models.Common;
using InventarioSaaS.Web.Models.Customers;
using InventarioSaaS.Web.Models.Payments;
using InventarioSaaS.Web.Models.Sales;

namespace InventarioSaaS.Web.Models.AccountsReceivable
{
    public class AccountReceivableDetailDto
    {
        public int Id { get; set; }

        public SaleDto Venta { get; set; }

        public CustomerDto Cliente { get; set; }

        public decimal MontoTotal { get; set; }

        public decimal MontoPendiente { get; set; }

        public TipoPago.Estado Estado { get; set; }

        public List<PaymentDto> Pagos { get; set; } = [];

        public DateTime FechaCreacion { get; set; }
    }
}
