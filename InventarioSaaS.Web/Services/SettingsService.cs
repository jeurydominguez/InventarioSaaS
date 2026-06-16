using InventarioSaaS.Web.Models.Common;
using InventarioSaaS.Web.Models.Settings;
using System.Net.Http.Json;

namespace InventarioSaaS.Web.Services
{
    public class SettingsService
    {
        private readonly IHttpClientFactory factory;
        private readonly HttpClient httpClient;

        public SettingsService(
            IHttpClientFactory factory, HttpClient httpClient)
        {
            this.factory = factory;
            this.httpClient = httpClient;
        }
        public async Task<EmpresaSettingsDto> ObtenerEmpresa()
        {
            var http = factory.CreateClient("Api");

            return await http.GetFromJsonAsync<EmpresaSettingsDto>(
                "api/settings/empresa")
                ?? new();
        }
        public async Task ActualizarEmpresa(EmpresaSettingsDto dto)
        {
            var http = factory.CreateClient("Api");

            var patch = new List<PatchOperation>
    {
        new()
        {
            op = "replace",
            path = "/nombre",
            value = dto.Nombre
        },

        new()
        {
            op = "replace",
            path = "/email",
            value = dto.Email
        }
    };

            var response = await http.PatchAsJsonAsync(
                "api/settings/empresa",
                patch);

            response.EnsureSuccessStatusCode();
        }
    }
}
