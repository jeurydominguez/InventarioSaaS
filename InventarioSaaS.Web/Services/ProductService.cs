using InventarioSaaS.Web.Models.Common;
using InventarioSaaS.Web.Models.Products;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace InventarioSaaS.Web.Services;

public class ProductService
{
    private readonly IHttpClientFactory _factory;
    private readonly HttpClient httpClient;

    public ProductService(
        IHttpClientFactory factory, HttpClient httpClient)
    {
        _factory = factory;
        this.httpClient = httpClient;
    }

    public async Task<PagedResponse<ProductDto>?>
        ObtenerProductos(
            int page = 1,
            int pageSize = 10,
            string? search = null,
            string? categoria = null)
    {
        var client =
            _factory.CreateClient("Api");

        var url =
            $"api/producto?page={page}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"&search={search}";
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            url += $"&categoria={categoria}";
        }

        return await client.GetFromJsonAsync<
            PagedResponse<ProductDto>>(url);
    }

    public async Task<ProductDetailDto?>
    ObtenerProductoPorId(int id)
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<ProductDetailDto>(
            $"api/producto/{id}");
    }
    public async Task<bool> EditarProducto(
        int id,
        EditProductDto model)
    {
        var patchOperations = new object[]
        {
        new
        {
            op = "replace",
            path = "/nombre",
            value = model.Nombre
        },
        new
        {
            op = "replace",
            path = "/precioVenta",
            value = model.PrecioVenta
        },
        new
        {
            op = "replace",
            path = "/precioCompra",
            value = model.PrecioCompra
        },
        new
        {
            op = "replace",
            path = "/stock",
            value = model.Stock
        },
        new
        {
            op = "replace",
            path = "/categoriaId",
            value = model.CategoriaId
        },
                new
        {
            op = "replace",
            path = "/foto",
            value = model.Foto
        },
        new
        {
            op = "replace",
            path = "/descripcion",
            value = model.Descripcion
        }
        };

        var json =
            JsonSerializer.Serialize(patchOperations);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json-patch+json");

        var client = _factory.CreateClient("Api");

        var response =
            await client.PatchAsync(
                $"api/producto/{id}",
                content);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> CrearProducto(CrearProductoDto model)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.PostAsJsonAsync(
                "api/producto",
                model);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> EliminarProducto(int id)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.DeleteAsync(
                $"api/producto/{id}");

        return response.IsSuccessStatusCode;
    }
    public async Task<InventarioStatsDto?>
    ObtenerStats()
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<
            InventarioStatsDto>(
            "api/producto/stats");
    }
    public async Task<List<ProductDto>> ObtenerTodos()
    {
        var client = _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<List<ProductDto>>
            ("api/producto/all")
            ?? [];
    }
}