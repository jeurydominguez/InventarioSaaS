using InventarioSaaS.Application.Domain;
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
            var venta = await appDbcontext.Venta
                .Include(x => x.Detalles)
                .Include(x=> x.cliente)
                .Where(v => v.EmpresaId == empresaId && v.Id == id)
                .FirstOrDefaultAsync();
            return venta;
        }

        public async Task CrearCuentaPorCobrar(CuentasPorCobrar cuenta)
        {
            appDbcontext.CuentasPorCobrar.Add(cuenta);
            await appDbcontext.SaveChangesAsync();
        }

        public async Task<PagedResponse<LeerVentasDto>> ObtenerP(int empresId, VentasQuery queryparams)
        {
            var query = appDbcontext.Venta
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.cliente)
                .Where(x => x.EmpresaId == empresId)
                .AsQueryable();

            if (queryparams.Fecha.HasValue)
            {
                query = query.Where(x =>
                    DateOnly.FromDateTime(x.Fecha)
                    == queryparams.Fecha.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryparams.QuerySeach))
            {
                var search =
                    queryparams.QuerySeach.Trim().ToLower();

                query = query.Where(x =>

                    x.cliente.Nombre.ToLower()
                        .Contains(search)

                    ||

                    x.Id.ToString()
                        .Contains(search)

                    ||

                    x.Usuario.NombreCompleto.ToLower()
                        .Contains(search)
                );
            }

            var respuesta = query

                .OrderByDescending(x => x.Fecha)

                .Select(x => new LeerVentasDto
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    clienteId = x.ClienteId,
                    NombreCliente = x.cliente.Nombre,
                    UsuarioId = x.UsuarioId,
                    NombreVendedor = x.Usuario.NombreCompleto,
                    TipoPago = x.TipoPago,
                    Total = x.Total
                });

            return await respuesta.PaginateAsync(
                queryparams.Page,
                queryparams.PageSize);
        }
    }
}
