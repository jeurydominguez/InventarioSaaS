using InventarioSaaS.Web.Models.Common;
using InventarioSaaS.Web.Models.Payments;
using System.Net.Http.Json;

namespace InventarioSaaS.Web.Services;

public class PaymentService
{
    private readonly IHttpClientFactory _factory;

    public PaymentService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<(bool Success, string Message)>
        CrearPago(CreatePaymentDto dto)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.PostAsJsonAsync(
                "api/pago",
                dto);

        if (response.IsSuccessStatusCode)
        {
            return (true, "");
        }

        var error =
            await response.Content.ReadAsStringAsync();

        return (false, error);
    }
    public async Task<PagedResponse<PaymentDto>?>
        ObtenerPagos(
            int page = 1,
            string search = "",
            int pageSize = 10)
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<
            PagedResponse<PaymentDto>>(
            $"api/pago?page={page}&pageSize={pageSize}&search={search}");
    }
}