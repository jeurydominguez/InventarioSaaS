using InventarioSaaS.Domain.DTO;
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
        Task<int> BuscarEmpresa();
        Task<Categoria> ObtenerPorId(int id, int empresaId);
        Task<List<Categoria>> Get(int empresaId);
        Task Crear(Categoria categoria);
        Task Eliminar(Categoria modelo);
        Task<Categoria> Buscar(int empresaId, CategoriaDto dto);
        Task<PagedResponse<LeerCategoriaDto>> Obtener(int empresaId, CategoriaQuery queryparams);
    }
}
