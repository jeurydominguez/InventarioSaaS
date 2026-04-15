using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IClienteService
    {
        Task Crear(CrearClienteDto dto);
        Task<LeerClienteDto> BuscarPorId(int id);
        Task<List<LeerClienteDto>> ObtenerTodos();
        Task<ActualizarClienteDto> Actualizar(int id);
        Task Editar(ActualizarClienteDto dto);
        Task Eliminar(int id);
    }
}
