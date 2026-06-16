using InventarioSaaS.Web.Models;
using InventarioSaaS.Web.Models.Reports;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace InventarioSaaS.Web.Services
{
    public class ReportService
    {
        private readonly IHttpClientFactory factory;
        public ReportService(IHttpClientFactory factory)
        {
            this.factory = factory;
        }
        public async Task<List<TopProductDto>?>
    ObtenerProductosTop(DateTime inicio, DateTime final)
        {
            var client =
                factory.CreateClient("Api");

            return await client.GetFromJsonAsync<
                List<TopProductDto>>(
                    $"api/reportes/productos-mas-vendidos?inicio={inicio:O}&final={final:O}");
        }
        public async Task<NetProfitDto?>ObtenerGananciaNeta(DateTime inicio, DateTime final)
        {
            var client =
                factory.CreateClient("Api");

            return await client.GetFromJsonAsync<
                NetProfitDto>(
                    $"api/reportes/ganancia-neta?inicio={inicio:O}&final={final:O}");
        }
        public async Task<ReporteResumenDto?> ObtenerResumen(DateTime inicio, DateTime final)
        {
            var client =
                factory.CreateClient("Api");

            return await client.GetFromJsonAsync<
                ReporteResumenDto>(
                    $"api/reportes/resumen?inicio={inicio:O}&final={final:O}");
        }
        public async Task<List<VentasChartDto>> ObtenerVentasChart(
                DateTime inicio,
                DateTime final)
        {
            var client = factory.CreateClient("Api");

            var response =
                await client.GetFromJsonAsync<List<VentasChartDto>>(
                    $"api/reportes/ventas-para-chart?inicio={inicio:O}&final={final:O}");

            return response ?? [];
        }
        public async Task<List<NotificacionDto>> ObtenerNotificaciones()
        {
            var client = factory.CreateClient("Api");

            return await client
                .GetFromJsonAsync<List<NotificacionDto>>(
                    "api/reportes/notificacion")
                ?? [];
        }
    }

}
