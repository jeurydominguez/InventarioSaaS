namespace InventarioSaaS.Web.Models.Payments
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int CuentasPorCobrarId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
