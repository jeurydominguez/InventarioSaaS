using InventarioSaaS.Domain.DTO;
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
    public class SettingsRepository : ISettingsRepository
    {
        private readonly AppDbcontext context;
        private readonly IHttpContextAccessor httpContext;

        public SettingsRepository(AppDbcontext context, IHttpContextAccessor httpContext)
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

        public async Task<Empresa>BuscarEmpresa(int empresaId)
        {
            var empresa = await context.Empresa.Where(e => e.Id == empresaId).FirstOrDefaultAsync();
            return empresa;
        }

        public async Task Actualizar(Empresa empresa)
        {
            context.Empresa.Update(empresa);
            await context.SaveChangesAsync();
        }

        public async Task<Usuario> BuscarUsuario()
        {
            var Email = httpContext.HttpContext.User.Claims.Where(c => c.Type == "Email").FirstOrDefault().Value;

            var usuario = await context.Users.Where(u => u.Id == Email).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
