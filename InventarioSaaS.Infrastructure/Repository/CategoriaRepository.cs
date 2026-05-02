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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbcontext context;
        private readonly IHttpContextAccessor httpContext;

        public CategoriaRepository(AppDbcontext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }

        public async Task<int> BuscarEmpresa()
        {
            var empresa = httpContext.HttpContext.User.Claims.Where(i => i.Type == "EmpresaId").FirstOrDefault().Value;
            int empresaId = int.Parse(empresa);
            return empresaId;
        }

        public async Task<Categoria> ObtenerPorId(int id, int empresaId)
        {
            var categoria = await context.Categoria.Where(c => c.EmpresaId == empresaId && c.Id == id).FirstOrDefaultAsync();
            return categoria;
        }

        public async Task<List<Categoria>> Get(int empresaId)
        {
            var categorias = await context.Categoria.Where(c => c.EmpresaId == empresaId).ToListAsync();
            return categorias;
        }

        public async Task Crear(Categoria categoria)
        {
            context.Categoria.Add(categoria);
            await context.SaveChangesAsync();
        }

        public async Task Eliminar(Categoria modelo)
        {
            context.Categoria.Remove(modelo);
            await context.SaveChangesAsync();
        }

        public async Task<Categoria>Buscar(int empresaId, CategoriaDto dto)
        {
            var categoria = await context.Categoria.Where(c => c.EmpresaId == empresaId && c.Nombre == dto.Nombre && c.Descripcion == dto.Descripcion).FirstOrDefaultAsync();
            return categoria;
        }
    }
}
