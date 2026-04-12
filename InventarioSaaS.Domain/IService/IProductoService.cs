using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IProductoService
    {
        Task Crear(CrearProductoDto dto);
        Task<List<LeerProductoDto>> BuscarTodos();
        Task Editar(int id, EditarProductoDto dto);
        Task<LeerProductoDto> BuscarProductoPorId(int id);
    }
}
