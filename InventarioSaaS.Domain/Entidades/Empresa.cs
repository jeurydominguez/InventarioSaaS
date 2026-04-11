using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; } 

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public required DateTime FechaCreacion { get; set; }

        public required string Estado { get; set; }
    }
}
