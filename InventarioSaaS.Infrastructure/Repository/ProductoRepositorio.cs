using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Text;
using InventarioSaaS.Domain.IRepository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
            dbcontext.Producto.Add(producto);
            await dbcontext.SaveChangesAsync();
        }

        public async Task<string> BuscarClaimEmpresaID()
        {
            var claim = httpContext.HttpContext.User.Claims.Where(i => i.Type == "EmpresaId").FirstOrDefault().Value;
            return claim;
        }

        public async Task<List<Producto>> BuscarTodos(int empresaId)
        {
            var productos = await dbcontext.Producto.Where(e => e.EmpresaId == empresaId).ToListAsync();
            return productos;
        }

        public async Task<Producto> BuscarProducto(int empresaId, int productoId)
        {
            var productoDb = await dbcontext.Producto.Include(c=> c.Categoria).Where(e => e.EmpresaId == empresaId && e.Id == productoId).FirstOrDefaultAsync();
            return productoDb;
            //modelar todo el proceso de producto con la propiedad de categoria 
        }

        public async Task Editar(Producto producto)
        {
            dbcontext.Producto.Update(producto);
            await dbcontext.SaveChangesAsync();
        }

        public async Task Eliminar(Producto modelo)
        {
            dbcontext.Producto.Remove(modelo);
            await dbcontext.SaveChangesAsync();
        }
    }
}
