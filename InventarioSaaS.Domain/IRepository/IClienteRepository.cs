using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.IRepository
{
    public interface IClienteRepository
    {
        Task<string> BuscarEmpresaId();
        Task<Cliente> ObtenerPorId(int empresaId, int id);
        Task Crear(Cliente modelo);
    }
}
