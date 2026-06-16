using System.Net.Http.Json;
using InventarioSaaS.Web.Models.AccountsReceivable;
using InventarioSaaS.Web.Models.Common;

namespace InventarioSaaS.Web.Services;

public class AccountsReceivableService
{
    private readonly IHttpClientFactory _factory;

    public AccountsReceivableService(
        IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<
        PagedResponse<LeerCuentasPorCobrarReportes>?>
        Obtener(
            int page = 1,
            string search = "",
            int pageSize = 10)
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<
            PagedResponse<LeerCuentasPorCobrarReportes>>(
                $"api/cuenta?page={page}&pageSize={pageSize}&search={search}");
    }
    public async Task<AccountReceivableDetailDto?>
    ObtenerPorId(int id)
    {
        var client =
            _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<
            AccountReceivableDetailDto>(
                $"api/cuenta/{id}");
    }
}