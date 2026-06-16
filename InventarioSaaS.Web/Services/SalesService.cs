using InventarioSaaS.Web.Models.Common;
using InventarioSaaS.Web.Models.Sales;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace InventarioSaaS.Web.Services;

public class SalesService
{
    private readonly IHttpClientFactory _factory;

    private readonly IJSRuntime _js;

    public SalesService(
        IHttpClientFactory factory,
        IJSRuntime js)
    {
        _factory = factory;
        _js = js;
    }

    public async Task<PagedResponse<SaleDto>?>
        ObtenerVentas(
            int page = 1,
            string search = "",
            int pageSize = 10)
    {
        var client = _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<
            PagedResponse<SaleDto>>(
            $"api/ventas?page={page}&pageSize={pageSize}&search={search}");
    }

    public async Task<LeerVentaDtoUnidad?>
        ObtenerVentaPorId(int id)
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<
            LeerVentaDtoUnidad>(
                $"api/ventas/{id}");
    }

    public async Task<(bool Success, string Message)>
        CrearVenta(CreateSaleDto model)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.PostAsJsonAsync(
                "api/ventas",
                model);

        if (response.IsSuccessStatusCode)
        {
            return (true, "");
        }

        var error =
            await response.Content.ReadAsStringAsync();

        return (false, error);
    }

    public async Task DescargarFactura(int id)
    {
        var client =
            _factory.CreateClient("Api");

        var bytes =
            await client.GetByteArrayAsync(
                $"api/ventas/{id}/pdf");

        using var streamRef =
            new DotNetStreamReference(
                stream: new MemoryStream(bytes));

        await _js.InvokeVoidAsync(
            "downloadFileFromStream",
            $"Factura-{id}.pdf",
            streamRef);
    }
}