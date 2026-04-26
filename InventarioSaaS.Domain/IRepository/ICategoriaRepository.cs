using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IRepository
{
    public interface ICategoriaRepository
    {
        Task<string> BuscarEmpresa();
        Task<Categoria> ObtenerPorId(int id, int empresaId);
        Task<List<Categoria>> Get(int empresaId);
        Task Crear(Categoria categoria);
    }
}
