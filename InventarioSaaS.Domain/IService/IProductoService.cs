using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IService
{
    public interface IProductoService
    {
        Task Crear(CrearProductoDto dto);
    }
}
