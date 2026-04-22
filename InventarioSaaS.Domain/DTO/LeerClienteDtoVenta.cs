using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class LeerClienteDtoVenta
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }

        [Phone]
        public string? NumeroTelefono { get; set; }

        public string? Direccion { get; set; }
    }
}
