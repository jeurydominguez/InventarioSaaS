namespace InventarioSaaS.Web.Models.Payments
{
    public class CreatePaymentDto
    {
        public int CuentaPorCobrarId { get; set; }

        public decimal Monto { get; set; }
    }
}
