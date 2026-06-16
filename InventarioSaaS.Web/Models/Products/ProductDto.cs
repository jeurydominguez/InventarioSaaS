namespace InventarioSaaS.Web.Models.Products;

public class ProductDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }

    public int CategoriaId { get; set; }

    public string? Foto { get; set; }
}