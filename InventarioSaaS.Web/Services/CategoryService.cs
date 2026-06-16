using System.Net.Http.Json;
using InventarioSaaS.Web.Models.Categories;

namespace InventarioSaaS.Web.Services;

public class CategoryService
{
    private readonly IHttpClientFactory factory;

    public CategoryService(IHttpClientFactory factory)
    {
        this.factory = factory;
    }

    public async Task<List<CategoryDto>> ObtenerCategorias()
    {
        var client = factory.CreateClient("Api");

        return await client
            .GetFromJsonAsync<List<CategoryDto>>(
                "api/categoria/all")
            ?? [];
    }

    public async Task<bool> Crear(CreateCategoryDto dto)
    {
        var client = factory.CreateClient("Api");

        var response =
            await client.PostAsJsonAsync(
                "api/categoria",
                dto);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Editar(EditCategoryDto dto)
    {
        var client = factory.CreateClient("Api");

        var response =
            await client.PutAsJsonAsync(
                $"api/categoria/{dto.Id}",
                dto);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Eliminar(int id)
    {
        var client = factory.CreateClient("Api");

        var response =
            await client.DeleteAsync(
                $"api/categoria/{id}");

        return response.IsSuccessStatusCode;
    }
}