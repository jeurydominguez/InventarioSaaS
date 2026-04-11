using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventarioSaaS.Domain.Entidades
{
    public class Usuario : IdentityUser
    {
        [Required]
        public required string NombreCompleto { get; set; }

        public int EmpresaId { get; set; }

        [Required]
        public required Empresa Empresa { get; set; }

        public required string Rol { get; set; }
    }
}
