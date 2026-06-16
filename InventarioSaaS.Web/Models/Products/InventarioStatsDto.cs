namespace InventarioSaaS.Web.Models.Products
{
    public class InventarioStatsDto
    {
        public int TotalProductos { get; set; }

        public int StockBajo { get; set; }

        public int TotalCategorias { get; set; }

        public decimal ValorInventario { get; set; }
    }
}
