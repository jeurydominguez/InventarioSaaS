namespace InventarioSaaS.Web.Models.Categories;

public class EditCategoryDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;
}