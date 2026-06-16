namespace InventarioSaaS.Web.Models.Common
{
    public class TipoPago
    {
        public enum EstadoVenta
        {
            Contado = 0,
            credito = 1
        }

        public enum Estado
        {
            Pendiente = 0,
            Pagado = 1
        }
    }
}
