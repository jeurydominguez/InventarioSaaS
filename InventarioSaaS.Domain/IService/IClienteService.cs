using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IClienteService
    {
        Task Crear(CrearClienteDto dto);
        Task<LeerClienteDto> BuscarPorId(int id);
    }
}
