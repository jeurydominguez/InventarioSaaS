using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace InventarioSaaS.Infrastructure.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbcontext dbcontext;
        private readonly IHttpContextAccessor httpContext;
        public ClienteRepository(AppDbcontext dbcontext, IHttpContextAccessor httpContext)
        {
            this.dbcontext = dbcontext;
            this.httpContext = httpContext;
        }

        public async Task<string> BuscarEmpresaId()
        {
            var claim = httpContext.HttpContext.User.Claims.Where(e => e.Type == "EmpresaId").FirstOrDefault().Value;
            return claim;
        }

        public async Task<Cliente> ObtenerPorId(int empresaId, int id)
        {
            var cliente = await dbcontext.Cliente.Where(e => e.EmpresaId == empresaId && e.Id == id).FirstOrDefaultAsync();
            return cliente;
        }

        public async Task Crear(Cliente modelo)
        {
            dbcontext.Cliente.Add(modelo);
            await dbcontext.SaveChangesAsync();
        }
    }
}
