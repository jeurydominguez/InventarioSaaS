using InventarioSaaS.Application.Domain;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Infrastructure.Repository
{
    public class CuentasPorCobrarRepository : CuentasPorCobrarIRepository
    {
        private readonly AppDbcontext dbcontext;
        private readonly IHttpContextAccessor httpContext;

        public CuentasPorCobrarRepository(AppDbcontext dbcontext, IHttpContextAccessor httpContext)
        {
            this.dbcontext = dbcontext;
            this.httpContext = httpContext;
        }

        public async Task<string> ObtenerEmpresaId()
        {
            var claim = httpContext.HttpContext!.User.Claims.Where(e => e.Type == "EmpresaId").FirstOrDefault().Value;
            return claim;
        }

        public async Task<List<CuentasPorCobrar>> Get(int empresaId)
        {
            var cuentas = await dbcontext.CuentasPorCobrar.Include(c=>c.Cliente).Include(v => v.Venta).Where(c => c.EmpresaId == empresaId).ToListAsync();
            return cuentas;
        }

        public async Task<CuentasPorCobrar> ObtenerUno(int empresaId, int id)
        {
            var cuenta = await dbcontext.CuentasPorCobrar.Include(c=>c.Pagos).Include(v => v.Venta)
                .Include(c => c.Cliente)
                .Where(c => c.EmpresaId == empresaId && c.Id == id)
                .FirstOrDefaultAsync();
            return cuenta;
        }

        public async Task<PagedResponse<LeerCuentasPorCobrarReportes>> Obtener(int empresaId, CuentrasPorCobrarQuery queryparams)
        {
            var query = dbcontext.CuentasPorCobrar
                .AsNoTracking()
                .OrderByDescending(x=> x.FechaCreacion)
                .Where(x => x.EmpresaId == empresaId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(queryparams.NombreCliente))
            {
                query = query.Where(x => x.Cliente.Nombre == queryparams.NombreCliente);
            }

            var resultado = query
                .Select(x => new LeerCuentasPorCobrarReportes
                {
                    Id = x.Id,
                    VentaID = x.VentaId,
                    MontoPendiente = x.MontoPendiente,
                    MontoTotal = x.MontoTotal,
                    Estado = x.Estado
                });
            return await resultado.PaginateAsync(queryparams.Page,
                queryparams.PageSize);
        }
    }
}
