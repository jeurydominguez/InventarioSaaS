using InventarioSaaS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IService
{
    public interface ISettingsService
    {
        Task<EmpresaSettingsDto> BuscarEmpresa();
        Task Editar(EmpresaSettingsDto dto);
        Task<UsuarioSettingsDto> BuscarUsuario();
    }
}
