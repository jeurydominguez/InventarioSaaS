using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IRepository
{
    public interface ISettingsRepository
    {
        Task<int> ObtenerEmpresaId();
        Task<Empresa> BuscarEmpresa(int empresaId);
        Task Actualizar(Empresa empresa);
        Task<Usuario> BuscarUsuario();
    }
}
