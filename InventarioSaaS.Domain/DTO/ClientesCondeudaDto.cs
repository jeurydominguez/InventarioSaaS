using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class ClientesCondeudaDto
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public string? NumeroTelefono { get; set; }

        public List<LeerCuentasPorCobrarReportes> Deudas { get; set; } = [];

        public string? Direccion { get; set; }
    }
}
