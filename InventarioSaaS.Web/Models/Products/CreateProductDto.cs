namespace InventarioSaaS.Web.Models.Products;

public class CrearProductoDto
{
    public string Nombre { get; set; } = "";

    public decimal PrecioVenta { get; set; }

    public decimal PrecioCompra { get; set; }

    public int Stock { get; set; }

    public string? Foto { get; set; }

    public int CategoriaId { get; set; }

    public string? Descripcion { get; set; }
}