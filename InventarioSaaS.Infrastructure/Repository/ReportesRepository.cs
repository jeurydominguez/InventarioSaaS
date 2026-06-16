using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Infrastructure.Repository
{
    public class ReportesRepository : IReportesRepository
    {
        private readonly AppDbcontext context;
        private readonly IHttpContextAccessor httpcontex;
        public ReportesRepository(AppDbcontext context, IHttpContextAccessor httpcontext)
        {
            this.context = context;
            this.httpcontex = httpcontext;
        }

        public async Task<string> BuscarEmpresa()
        {
            var empresa = httpcontex.HttpContext.User.Claims.Where(e => e.Type == "EmpresaId").FirstOrDefault().Value;
            return empresa;
        } 

        public async Task<List<Venta>> VentasPorDia(DateTime inicio, DateTime final, int empresaID)
        {
            var ventas = await context.Venta.Include(d=> d.Detalles)
                .Include(c=> c.cliente)
                .Include(u=> u.CuentaPorCobrar)
                .Where(v => v.Fecha >= inicio && v.Fecha < final && v.EmpresaId == empresaID)
                .ToListAsync();
            return ventas;
        }

        public async Task<List<ProductoTop5Dto>> ProductosMasVendidos(DateTime inicio, DateTime final, int empresaId)
        {
            var top5 = await context.Venta
                .Where(v => v.Fecha >= inicio && v.Fecha < final && v.EmpresaId == empresaId)
                .SelectMany(v => v.Detalles)
                .GroupBy(d => new { d.ProductoId, d.Producto.Nombre })
                .Select(g => new ProductoTop5Dto
                {
                    Id = g.Key.ProductoId,
                    Nombre = g.Key.Nombre,
                    CantidadVendida = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .Take(5)
                .ToListAsync();

            return top5;
        }

        public async Task<List<Cliente>> ClientesConDeuda(int empresaId)
        {
            var clientes = await context.Cliente.Where(c => c.Deudas.Any(x => x.Estado == TipoPago.Estado.Pendiente) && c.EmpresaId == empresaId)
                .Include(d => d.Deudas.Where(s => s.Estado == TipoPago.Estado.Pendiente)).ToListAsync();
            return clientes;
        }

        public async Task<EstadoCuentasDtos> EstadoDeCuentasPorCobrar(int empresaId)
        {
            var resumen = await context.CuentasPorCobrar
            .Where(c => c.EmpresaId == empresaId)
            .Select(c => new
            {
                Total = c.MontoTotal,
                Pagado = c.Pagos.Sum(p => (decimal?)p.Monto) ?? 0
            })
            .GroupBy(x => 1)
            .Select(g => new EstadoCuentasDtos
            {
                Total = g.Sum(x => x.Total),
                Pagado = g.Sum(x => x.Pagado),
                Pendiente = g.Sum(x => x.Total - x.Pagado)
            })
            .FirstOrDefaultAsync();
            return resumen ?? new EstadoCuentasDtos();
        }
        public async Task<GananciaNetaDto> ObtenerGanaciaNeta(
            DateTime inicio,
            DateTime final,
            int empresaId)
        {
            var ingresos = await context.Venta
                .Where(v =>
                    v.EmpresaId == empresaId &&
                    v.Fecha >= inicio &&
                    v.Fecha <= final)
                .SumAsync(v => v.Total);

            var gastos = await context.Detalle
                .Where(d =>
                    d.Venta.EmpresaId == empresaId &&
                    d.Venta.Fecha >= inicio &&
                    d.Venta.Fecha <= final)
                .SumAsync(d =>
                    d.PrecioCompra * d.Cantidad);

            return new GananciaNetaDto
            {
                Ingresos = ingresos,
                Gastos = gastos,
                GananciaNeta = ingresos - gastos
            };
        }
        public async Task<ReporteResumenDto>ObtenerResumen(DateTime inicio, DateTime final, int empresaId)
        {
            var facturas = await context.Venta.Where(x => x.EmpresaId == empresaId && x.Fecha >= inicio && x.Fecha <= final).ToListAsync();

            var ingresos = facturas.Sum(v => v.Total);

            var gastos = facturas.Sum(x => x.Detalles.Sum(d => d.PrecioCompra * d.Cantidad));

            var clientes = await context.Cliente.CountAsync(c => c.EmpresaId == empresaId);

            var totalFactura = facturas.Count;

            decimal convercion = 0;

            if (clientes > 0)
            {
                convercion = ((decimal)totalFactura / clientes) * 100;
            }

            return new ReporteResumenDto
            {
                Ingresos = ingresos,
                Gastos = gastos,
                Facturas = totalFactura,
                Clientes = clientes,
                Conversion = convercion
            };
        }
        public async Task<List<VentaChartDto>> VentaPorRango(
    DateTime inicio,
    DateTime final,
    int empresaId)
        {
            var ventas = await context.Venta
                .Where(v =>
                    v.EmpresaId == empresaId &&
                    v.Fecha >= inicio &&
                    v.Fecha <= final)
                .ToListAsync();

            var resultado = ventas
                .GroupBy(v => v.Fecha.Date)
                .Select(g => new VentaChartDto
                {
                    Label = g.Key.ToString("dd/MM"),
                    Total = g.Sum(v => v.Total)
                })
                .OrderBy(x => x.Label)
                .ToList();

            return resultado;
        }
        public async Task<List<NotificacionDto>>ObtenerNotificaciones(int empresaId)
        {
            var resultado = new List<NotificacionDto>();

            // STOCK BAJO

            var stockBajo = await context.Producto
                .Where(p =>
                    p.EmpresaId == empresaId &&
                    p.Stock > 0 &&
                    p.Stock <= 2)
                .ToListAsync();

            resultado.AddRange(
                stockBajo.Select(p => new NotificacionDto
                {
                    Titulo = "Stock bajo",
                    Mensaje = $"{p.Nombre} tiene {p.Stock} unidades",
                    Tipo = "warning",
                    Fecha = DateTime.UtcNow
                }));

            // AGOTADOS

            var agotados = await context.Producto
                .Where(p =>
                    p.EmpresaId == empresaId &&
                    p.Stock <= 0)
                .ToListAsync();

            resultado.AddRange(
                agotados.Select(p => new NotificacionDto
                {
                    Titulo = "Producto agotado",
                    Mensaje = $"{p.Nombre} está agotado",
                    Tipo = "danger",
                    Fecha = DateTime.UtcNow
                }));

            return resultado;
        }
    }
}
