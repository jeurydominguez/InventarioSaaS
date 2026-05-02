using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Infrastructure.Repository
{
    public class PagoRepository : IPagoRepository
    {
        private readonly AppDbcontext context;
        private readonly IHttpContextAccessor httpContext;
        public PagoRepository(AppDbcontext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }

        public async Task<int> ObtenerEmpresaId()
        {
            var empresa = httpContext.HttpContext.User.Claims.Where(c => c.Type == "EmpresaId").FirstOrDefault().Value;
            int empresaId = int.Parse(empresa);
            return empresaId;
        }

        public async Task<List<Pago>> GetAll(int empresaId)
        {
            var pagos = await context.Pago.Where(p => p.EmpresaId == empresaId).ToListAsync();
            return pagos;
        }

        public async Task<Pago> ObtenerPorId(int empresaId, int id)
        {
            var pago = await context.Pago.Include(c=> c.CuentaPorCobrar).Where(p => p.EmpresaId == empresaId && p.Id == id).FirstOrDefaultAsync();
            return pago;
        }

        public async Task<CuentasPorCobrar> ObtenerCuentaPorCobrar(int empresaId, int cuentaId)
        {
            var cuenta = await context.CuentasPorCobrar.Where(e => e.EmpresaId == empresaId && e.Id == cuentaId).FirstOrDefaultAsync();
            return cuenta;
        }

        public async Task GuardarPago(Pago pago, CuentasPorCobrar cuenta)
        {
            context.Pago.Add(pago);
            context.CuentasPorCobrar.Update(cuenta);
            await context.SaveChangesAsync();
        }
    }
}
