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

        public async Task<string> BuscarEmpresa()
        {
            var empresa = httpContext.HttpContext!.User.Claims.Where(e => e.Type == "EmpresaId").FirstOrDefault().Value;
            return empresa;
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

        public async Task Eliminar(int id)
        {

        }
    }
}
