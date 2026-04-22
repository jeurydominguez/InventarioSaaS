using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Phone]
        public string? NumeroTelefono { get; set; }

        public List<Venta> Facturas { get; set; } = [];

        public string? Direccion { get; set; }

        public int EmpresaId { get; set; }
    }
}
