using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IProductoService
    {
        Task Crear(CrearProductoDto dto);
        Task<List<LeerProductoDto>> BuscarTodos();
        Task<EditarProductoDto> Editar(int id);
        Task Actualizar(EditarProductoDto dto);
        Task<LeerProductoDtoUnidad> BuscarProductoPorId(int id);
        Task Eliminar(int id);
    }
}
