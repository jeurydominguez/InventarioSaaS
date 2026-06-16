using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Application.Mapper
{
    public class EmpresaMapper
    {
        public static EmpresaSettingsDto AEmpresaSettingsDto(Empresa empresa)
        {
            return new EmpresaSettingsDto
            {
                Nombre = empresa.Nombre,
                Email = empresa.Email
            };
        }
    }
}
