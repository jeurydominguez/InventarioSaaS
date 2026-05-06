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
    }
}
