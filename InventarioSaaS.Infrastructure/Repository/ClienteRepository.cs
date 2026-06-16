using InventarioSaaS.Application.Domain;
using InventarioSaaS.Domain.DTO;
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
            var cliente = await dbcontext.Cliente.Include(c=>c.Facturas).Where(e => e.EmpresaId == empresaId && e.Id == id).FirstOrDefaultAsync();
            return cliente;
        }

        public async Task Crear(Cliente modelo)
        {
            dbcontext.Cliente.Add(modelo);
            await dbcontext.SaveChangesAsync();
        }

        public async Task<List<Cliente>> ObtenerTodo(int id)
        {
            var clientes = await dbcontext.Cliente.Where(e => e.EmpresaId == id).ToListAsync();
            return clientes;
        }

        public async Task Actualizar(Cliente cliente)
        {
            dbcontext.Cliente.Update(cliente);
            await dbcontext.SaveChangesAsync();
        }

        public async Task Eliminar(Cliente modelo)
        {
            dbcontext.Cliente.Remove(modelo);
            await dbcontext.SaveChangesAsync();
        }

        public async Task<PagedResponse<LeerClienteDtoVenta>>Obtener(int empresaId, ClienteQuery queryparams)
        {
            var query = dbcontext.Cliente
                .AsNoTracking()
                .Where(x => x.EmpresaId == empresaId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(queryparams.Search))
            {
                query = query.Where(x => x.Nombre == queryparams.Search);
            }

            if (!string.IsNullOrEmpty(queryparams.Telefono))
            {
                query = query.Where(x => x.NumeroTelefono == queryparams.Telefono);
            }

            var resultado = query
                .OrderBy(x => x.Nombre)
                .Select(x => new LeerClienteDtoVenta
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    NumeroTelefono = x.NumeroTelefono,
                    Direccion = x.Direccion
                });

            return await resultado.PaginateAsync(
                queryparams.Page,
                queryparams.PageSize);
        }
    }
}
