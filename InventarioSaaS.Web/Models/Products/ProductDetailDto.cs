using InventarioSaaS.Web.Models.Categories;

namespace InventarioSaaS.Web.Models.Products;

public class ProductDetailDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public decimal PrecioVenta { get; set; }

    public decimal PrecioCompra { get; set; }

    public int Stock { get; set; }

    public int EmpresaId { get; set; }

    public int CategoriaId { get; set; }

    public CategoryDto Categoria { get; set; }
    public string? Foto { get; set; }

    public string? Descripcion { get; set; }
}