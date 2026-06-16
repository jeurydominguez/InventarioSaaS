using static InventarioSaaS.Web.Models.Common.TipoPago;

namespace InventarioSaaS.Web.Models.AccountsReceivable
{
    public class LeerCuentasPorCobrarReportes
    {
        public int Id { get; set; }

        public int VentaID { get; set; }

        public decimal MontoTotal { get; set; }

        public decimal MontoPendiente { get; set; }

        public Estado Estado { get; set; }
    }
}
