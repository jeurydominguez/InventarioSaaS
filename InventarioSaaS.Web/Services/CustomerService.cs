using InventarioSaaS.Web.Models.Common;
using InventarioSaaS.Web.Models.Customers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace InventarioSaaS.Web.Services;

public class CustomerService
{
    private readonly IHttpClientFactory _factory;

    public CustomerService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<PagedResponse<CustomerDto>?>
        ObtenerClientes(
            int page = 1,
            int pageSize = 10,
            string? search = null)
    {
        var client = _factory.CreateClient("Api");

        var url =
            $"api/cliente?page={page}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"&search={search}";
        }

        return await client.GetFromJsonAsync<
            PagedResponse<CustomerDto>>(url);
    }
    public async Task<bool> CrearCliente(
    CreateCustomerDto model)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.PostAsJsonAsync(
                "api/cliente",
                model);

        return response.IsSuccessStatusCode;
    }
    public async Task<LeerClienteDto?> ObtenerClientePorId(int id)
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<LeerClienteDto>(
            $"api/cliente/{id}");
    }
    public async Task<bool> EditarCliente(
    UpdateCustomerDto model)
    {
        var client =
            _factory.CreateClient("Api");

        var patchDoc = new[]
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
            path = "/numeroTelefono",
            value = model.NumeroTelefono
        },
        new
        {
            op = "replace",
            path = "/direccion",
            value = model.Direccion
        }
    };

        var response = await client.PatchAsJsonAsync(
            $"api/cliente/{model.Id}",
            patchDoc);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> EliminarCliente(int id)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.DeleteAsync(
                $"api/cliente/{id}");

        return response.IsSuccessStatusCode;
    }
}