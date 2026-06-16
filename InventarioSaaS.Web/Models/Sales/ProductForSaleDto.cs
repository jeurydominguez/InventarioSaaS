namespace InventarioSaaS.Web.Models.Sales
{
    public class ProductForSaleDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public string? Foto { get; set; }

        public int Cantidad { get; set; }
    }
}
