using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class ActualizarClienteDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        [Phone]
        public string? NumeroTelefono { get; set; }

        public string? Direccion { get; set; }

        public int EmpresaId { get; set; }
    }
}
