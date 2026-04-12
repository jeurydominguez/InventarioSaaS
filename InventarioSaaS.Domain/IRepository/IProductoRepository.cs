using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace InventarioSaaS.Domain.IRepository
{
    public interface IProductoRepository
    {
        Task Crear(Producto producto);
        Task<string> BuscarClaimEmpresaID();
        Task<List<Producto>> BuscarTodos(int empresaId);
    }
}
