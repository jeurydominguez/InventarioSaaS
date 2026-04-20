using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace InventarioSaaS.Infrastructure.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbcontext appDbcontext;
        private readonly UserManager<Usuario> userManager;
        private readonly IHttpContextAccessor httpContext;

        public VentaRepository(AppDbcontext appDbcontext, UserManager<Usuario> userManager, IHttpContextAccessor httpContext)
        {
            this.appDbcontext = appDbcontext;
            this.userManager = userManager;
            this.httpContext = httpContext;
        }

        public async Task<string> BuscarEmpresaId()
        {
            var empresaId = httpContext.HttpContext!.User.Claims.Where(e => e.Type == "EmpresaId").FirstOrDefault()!.Value;
            return empresaId;
        }

        public async Task<string> BuscarUsuarioId()
        {
            var usuarioId = httpContext.HttpContext!.User.Claims.Where(u => u.Type == "Id").FirstOrDefault()!.Value;
            return usuarioId;
        }

        public async Task<Dictionary<int, Producto>> BuscarProductos(List<ProductoParaVentaDto> productosDto, int empresaId)
        {
            var ids = productosDto.Select(x => x.Id).ToList();

            var productos = await appDbcontext.Producto.Where(p => p.EmpresaId == empresaId && ids.Contains(p.Id)).ToListAsync();

            return productos.ToDictionary(p => p.Id, p => p);
        }

        public async Task CrearVenta(Venta venta)
        {
            appDbcontext.Venta.Add(venta);
            await appDbcontext.SaveChangesAsync();
        }

        public async Task CrearDetalle(List<DetalleVenta> detalles)
        {
            await appDbcontext.AddRangeAsync(detalles);
            await appDbcontext.SaveChangesAsync();
        }

        public async Task ActualizarStock(Dictionary<int, Producto> produtos)
        {
            appDbcontext.Producto.UpdateRange(produtos.Values);
            await appDbcontext.SaveChangesAsync();
        }

        public async Task<List<Venta>> GetAll(int empresaId)
        {
            var ventas = await appDbcontext.Venta.Where(v => v.EmpresaId == empresaId).ToListAsync();
            return ventas;
        }

        public async Task<Venta> Obtener(int id, int empresaId)
        {
            var venta = await appDbcontext.Venta.Include(x => x.Detalles).Where(v => v.EmpresaId == empresaId && v.Id == id).FirstOrDefaultAsync();
            return venta;
        }
    }
}
