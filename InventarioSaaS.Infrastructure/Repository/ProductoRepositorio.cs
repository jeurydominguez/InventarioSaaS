using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Text;
using InventarioSaaS.Domain.IRepository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace InventarioSaaS.Infrastructure.Repository
{
    public class ProductoRepositorio : IProductoRepository
    {
        private readonly AppDbcontext dbcontext;
        private readonly IHttpContextAccessor httpContext;
        public ProductoRepositorio(AppDbcontext dbcontext, IHttpContextAccessor httpContext)
        {
            this.dbcontext = dbcontext;
            this.httpContext = httpContext;
        }

        public async Task Crear(Producto producto)
        {
            await dbcontext.Producto.AddAsync(producto);
            dbcontext.SaveChanges();
        }

        public async Task<string> BuscarClaimEmpresaID()
        {
            var claim = httpContext.HttpContext.User.Claims.Where(i => i.Type == "EmpresaId").FirstOrDefault().Value;
            return claim;
        }
    }
}
