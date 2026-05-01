using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IService
{
    public interface ICategoriaService
    {
        Task Crear(CategoriaDto dto);
        Task<CategoriaDto> ObtenerPorId(int Id);
        Task<List<LeerCategoriaDto>> Get();
        Task Eliminar(int id);
    }
}
