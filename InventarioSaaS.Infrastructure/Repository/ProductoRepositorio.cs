using InventarioSaaS.Application.Domain;
using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

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

        public async Task<int> BuscarClaimEmpresaID()
        {
            var claim = httpContext.HttpContext.User.Claims.Where(i => i.Type == "EmpresaId").FirstOrDefault().Value;
            int empresa = int.Parse(claim);
            return empresa;
        }

        public async Task<List<Producto>> BuscarTodos(int empresaId)
        {
            var productos = await dbcontext.Producto.Where(e => e.EmpresaId == empresaId).ToListAsync();
            return productos;
        }
        public async Task<InventarioStatsDto> BuscarStats(int empresaId)
        {
            var produtos = dbcontext.Producto.Where(x => x.EmpresaId == empresaId);

            return new InventarioStatsDto
            {
                TotalProductos = await produtos.CountAsync(),

                StockBajo = await produtos.CountAsync(x => x.Stock < 10),

                ValorInventario = await produtos.SumAsync(x => x.PrecioVenta * x.Stock),

                TotalCategorias = await dbcontext.Categoria.CountAsync(x => x.EmpresaId == empresaId)
            };
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

        public async Task<Categoria> BuscarCategoria(int empresaId, int id)
        {
            var categoria = await dbcontext.Categoria.Where(c => c.EmpresaId == empresaId && c.Id == id).FirstOrDefaultAsync();
            return categoria;
        }
        public async Task<PagedResponse<LeerProductoDto>> Obtener(int empresaId, ProductoQuery queryParams)
        {
            var query = dbcontext.Producto
                .AsNoTracking()
                .Where(x=> x.EmpresaId == empresaId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                query = query.Where(x =>
                    x.Nombre.Contains(queryParams.Search));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Categoria))
            {
                query = query.Where(x =>
                    x.Categoria.Nombre == queryParams.Categoria);
            }


            var resultado = query
                .OrderBy(x => x.Nombre)
                .Select(x => new LeerProductoDto
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    PrecioVenta = x.PrecioVenta,
                    Stock = x.Stock,
                    CategoriaId = x.CategoriaId,
                    Foto = x.Foto
                });

            return await resultado.PaginateAsync(
                queryParams.Page,
                queryParams.PageSize);
        }
    }
}
