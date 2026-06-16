using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class UsuarioActualDto
    {
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Rol { get; set; } = "";
        public string Iniciales { get; set; } = "";
        public string NombreEmpresa { get; set; } = "";
    }
}
